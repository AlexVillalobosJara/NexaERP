using AutoMapper;
using Core.Constants;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Security;
using Core.Enums;
using Core.Interfaces.Services.Security;
using Core.DTOs.Security;
using System.Security.Claims;
using NexaERP.Infrastructure.Data.Context;

namespace Application.Services.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NexaErpDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AuthenticationService(
            NexaErpDbContext context,
            IPasswordService passwordService,
            IJwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _context = context;
            _passwordService = passwordService;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResultDTO> LoginAsync(LoginDTO loginRequest)
        {
            try
            {
                // Buscar usuario por email
                var usuario = await _context.Usuarios
                    .Include(u => u.Empresa)
                    .FirstOrDefaultAsync(u => u.Email == loginRequest.Email.ToLower().Trim());

                if (usuario == null)
                {
                    await RegistrarEventoAuditoria(null, loginRequest.Email, TipoEvento.LoginFailed, false, "Usuario no encontrado");
                    return new AuthenticationResultDTO
                    {
                        IsSuccess = false,
                        ErrorMessage = "Credenciales inválidas"
                    };
                }

                // Verificar si la cuenta está activa
                if (!usuario.Activo)
                {
                    await RegistrarEventoAuditoria(usuario.Id, loginRequest.Email, TipoEvento.LoginFailed, false, "Cuenta inactiva");
                    return new AuthenticationResultDTO
                    {
                        IsSuccess = false,
                        ErrorMessage = "Cuenta inactiva. Contacte al administrador."
                    };
                }

                // Verificar si la cuenta está bloqueada
                if (usuario.CuentaBloqueada)
                {
                    await RegistrarEventoAuditoria(usuario.Id, loginRequest.Email, TipoEvento.LoginFailed, false, "Cuenta bloqueada");
                    return new AuthenticationResultDTO
                    {
                        IsSuccess = false,
                        ErrorMessage = "Cuenta bloqueada. Contacte al administrador.",
                        IsAccountLocked = true
                    };
                }

                // Verificar contraseña
                if (!_passwordService.VerifyPassword(loginRequest.Password, usuario.PasswordHash, usuario.Salt))
                {
                    // Incrementar intentos fallidos
                    usuario.IntentosLoginFallidos++;
                    usuario.FechaUltimoLoginFallido = DateTime.UtcNow;

                    // Bloquear cuenta si excede el máximo
                    var empresaConfig = await _context.Empresas.FindAsync(usuario.EmpresaId);
                    var maxIntentos = empresaConfig?.MaximoIntentosLogin ?? SecurityConstants.DefaultMaxLoginAttempts;

                    if (usuario.IntentosLoginFallidos >= maxIntentos)
                    {
                        usuario.CuentaBloqueada = true;
                        usuario.FechaBloqueoCuenta = DateTime.UtcNow;
                        await RegistrarEventoAuditoria(usuario.Id, loginRequest.Email, TipoEvento.AccountLocked, true, "Cuenta bloqueada por exceso de intentos");
                    }

                    await _context.SaveChangesAsync();
                    await RegistrarEventoAuditoria(usuario.Id, loginRequest.Email, TipoEvento.LoginFailed, false, "Contraseña incorrecta");

                    return new AuthenticationResultDTO
                    {
                        IsSuccess = false,
                        ErrorMessage = "Credenciales inválidas",
                        IsAccountLocked = usuario.CuentaBloqueada
                    };
                }

                // Verificar 2FA si está habilitado
                if (usuario.AutenticacionDobleFactorHabilitada)
                {
                    if (string.IsNullOrEmpty(loginRequest.TwoFactorCode))
                    {
                        return new AuthenticationResultDTO
                        {
                            IsSuccess = false,
                            RequiresTwoFactor = true,
                            ErrorMessage = "Código de verificación requerido"
                        };
                    }

                    // TODO: Implementar validación de código 2FA
                    // Por ahora, asumimos que es válido si se proporciona
                }

                // Login exitoso - resetear intentos fallidos
                usuario.IntentosLoginFallidos = 0;
                usuario.FechaUltimoLoginFallido = null;
                usuario.FechaUltimoLogin = DateTime.UtcNow;

                // Generar claims
                var claims = await GenerateUserClaims(usuario);
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

                // Generar tokens
                var token = _jwtTokenService.GenerateToken(claimsPrincipal);
                var refreshToken = _jwtTokenService.GenerateRefreshToken();
                var expiresAt = _jwtTokenService.GetTokenExpiration(token);

                // Crear sesión
                await CrearSesionUsuario(usuario.Id, token, refreshToken, expiresAt);

                await _context.SaveChangesAsync();
                await RegistrarEventoAuditoria(usuario.Id, usuario.Email, TipoEvento.LoginSuccess, true);

                var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
                usuarioDTO.EmpresaNombre = usuario.Empresa.RazonSocial;
                usuarioDTO.EmpresaRut = usuario.Empresa.RutEmpresa;

                return new AuthenticationResultDTO
                {
                    IsSuccess = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = expiresAt,
                    User = usuarioDTO,
                    RequiresPasswordChange = usuario.RequiereCambioPassword
                };
            }
            catch (Exception ex)
            {
                await RegistrarEventoAuditoria(null, loginRequest.Email, TipoEvento.LoginFailed, false, $"Error interno: {ex.Message}");
                return new AuthenticationResultDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "Error interno del servidor"
                };
            }
        }

        public async Task<AuthenticationResultDTO> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var sesion = await _context.SesionesUsuario
                    .Include(s => s.Usuario)
                    .ThenInclude(u => u.Empresa)
                    .FirstOrDefaultAsync(s => s.TokenSesion == refreshToken && s.SesionActiva);

                if (sesion == null || sesion.FechaExpiracion < DateTime.UtcNow)
                {
                    return new AuthenticationResultDTO { IsSuccess = false, ErrorMessage = "Token de actualización inválido" };
                }

                // Generar nuevo token
                var claims = await GenerateUserClaims(sesion.Usuario);
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                var newToken = _jwtTokenService.GenerateToken(claimsPrincipal);
                var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
                var expiresAt = _jwtTokenService.GetTokenExpiration(newToken);

                // Actualizar sesión
                sesion.TokenSesion = newRefreshToken;
                sesion.FechaExpiracion = expiresAt;
                sesion.FechaUltimaActividad = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var usuarioDTO = _mapper.Map<UsuarioDTO>(sesion.Usuario);
                usuarioDTO.EmpresaNombre = sesion.Usuario.Empresa.RazonSocial;
                usuarioDTO.EmpresaRut = sesion.Usuario.Empresa.RutEmpresa;

                return new AuthenticationResultDTO
                {
                    IsSuccess = true,
                    Token = newToken,
                    RefreshToken = newRefreshToken,
                    ExpiresAt = expiresAt,
                    User = usuarioDTO
                };
            }
            catch
            {
                return new AuthenticationResultDTO { IsSuccess = false, ErrorMessage = "Error al actualizar token" };
            }
        }

        public async Task LogoutAsync(int usuarioId, string tokenSesion)
        {
            try
            {
                var sesion = await _context.SesionesUsuario
                    .FirstOrDefaultAsync(s => s.UsuarioId == usuarioId && s.TokenSesion == tokenSesion && s.SesionActiva);

                if (sesion != null)
                {
                    sesion.SesionActiva = false;
                    sesion.FechaCierre = DateTime.UtcNow;
                    sesion.RazonCierre = "LOGOUT";
                    await _context.SaveChangesAsync();
                }

                await RegistrarEventoAuditoria(usuarioId, null, TipoEvento.Logout, true);
            }
            catch
            {
                // Log error but don't throw
            }
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var principal = _jwtTokenService.ValidateToken(token);
                return principal.Identity?.IsAuthenticated == true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UsuarioDTO?> GetCurrentUserAsync(string token)
        {
            try
            {
                var principal = _jwtTokenService.ValidateToken(token);
                if (principal.Identity?.IsAuthenticated != true)
                    return null;

                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var userId))
                    return null;

                var usuario = await _context.Usuarios
                    .Include(u => u.Empresa)
                    .FirstOrDefaultAsync(u => u.Id == userId && u.Activo);

                if (usuario == null)
                    return null;

                var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
                usuarioDTO.EmpresaNombre = usuario.Empresa.RazonSocial;
                usuarioDTO.EmpresaRut = usuario.Empresa.RutEmpresa;

                return usuarioDTO;
            }
            catch
            {
                return null;
            }
        }

        private async Task<List<Claim>> GenerateUserClaims(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new(ClaimTypes.Name, usuario.NombreUsuario),
                new(ClaimTypes.Email, usuario.Email),
                new(SecurityConstants.EmpresaIdClaim, usuario.EmpresaId.ToString()),
                new(SecurityConstants.FullNameClaim, usuario.NombreCompleto),
                new(SecurityConstants.TwoFactorClaim, usuario.AutenticacionDobleFactorHabilitada.ToString())
            };

            // Agregar roles y permisos
            var roles = await _context.UsuariosRoles
                .Include(ur => ur.Rol)
                .ThenInclude(r => r.RolPermisos)
                .ThenInclude(rp => rp.Permiso)
                .Where(ur => ur.UsuarioId == usuario.Id && ur.Activo &&
                            (ur.FechaVencimiento == null || ur.FechaVencimiento > DateTime.UtcNow))
                .ToListAsync();

            foreach (var usuarioRol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, usuarioRol.Rol.Nombre));

                if (usuarioRol.SucursalId.HasValue)
                    claims.Add(new Claim(SecurityConstants.SucursalIdClaim, usuarioRol.SucursalId.Value.ToString()));

                if (usuarioRol.BodegaId.HasValue)
                    claims.Add(new Claim(SecurityConstants.BodegaIdClaim, usuarioRol.BodegaId.Value.ToString()));

                // Agregar permisos del rol
                foreach (var rolPermiso in usuarioRol.Rol.RolPermisos.Where(rp => rp.Concedido))
                {
                    claims.Add(new Claim("permission", rolPermiso.Permiso.Codigo));
                }
            }

            return claims;
        }

        private async Task CrearSesionUsuario(int usuarioId, string token, string refreshToken, DateTime expiresAt)
        {
            var sesion = new SesionUsuario
            {
                UsuarioId = usuarioId,
                TokenSesion = refreshToken, // El refresh token se guarda en la base
                FechaInicioSesion = DateTime.UtcNow,
                FechaExpiracion = expiresAt,
                FechaUltimaActividad = DateTime.UtcNow,
                DireccionIP = "127.0.0.1", // TODO: Obtener IP real
                UserAgent = "NexaERP Web", // TODO: Obtener user agent real
                SesionActiva = true
            };

            _context.SesionesUsuario.Add(sesion);

            // Limitar sesiones concurrentes
            var sesionesActivas = await _context.SesionesUsuario
                .Where(s => s.UsuarioId == usuarioId && s.SesionActiva)
                .OrderByDescending(s => s.FechaUltimaActividad)
                .Skip(SecurityConstants.MaxConcurrentSessions - 1)
                .ToListAsync();

            foreach (var sesionAntigua in sesionesActivas)
            {
                sesionAntigua.SesionActiva = false;
                sesionAntigua.FechaCierre = DateTime.UtcNow;
                sesionAntigua.RazonCierre = "FORCE_CLOSE";
            }
        }

        private async Task RegistrarEventoAuditoria(int? usuarioId, string? email, TipoEvento tipoEvento, bool exitoso, string? mensaje = null)
        {
            var auditoria = new AuditoriaAcceso
            {
                UsuarioId = usuarioId,
                Email = email,
                TipoEvento = tipoEvento,
                FechaEvento = DateTime.UtcNow,
                DireccionIP = "127.0.0.1", // TODO: Obtener IP real
                UserAgent = "NexaERP Web", // TODO: Obtener user agent real
                Exitoso = exitoso,
                MensajeError = mensaje
            };

            _context.AuditoriaAccesos.Add(auditoria);
        }
    }
}
