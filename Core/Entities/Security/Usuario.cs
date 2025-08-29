using Core.Entities.Organization;
using NexaERP.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Security
{
    /// <summary>
    /// Entidad Usuario - Sistema de autenticación multi-tenant
    /// Implementa la especificación técnica de seguridad de NexaERP
    /// </summary>
    public class Usuario : TenantEntity
    {
        // Datos de Autenticación
        [Required]
        [MaxLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Salt { get; set; } = string.Empty;

        // Datos Personales
        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [MaxLength(12)]
        public string? RutPersona { get; set; }

        // Configuración de Seguridad
        public bool RequiereCambioPassword { get; set; } = false;
        public DateTime? FechaExpiracionPassword { get; set; }
        public int IntentosLoginFallidos { get; set; } = 0;
        public DateTime? FechaUltimoLoginFallido { get; set; }
        public bool CuentaBloqueada { get; set; } = false;
        public DateTime? FechaBloqueoCuenta { get; set; }

        // Configuración de 2FA
        public bool AutenticacionDobleFactorHabilitada { get; set; } = false;
        [MaxLength(255)]
        public string? SecretoTOTP { get; set; }
        public string? CodigosRecuperacion { get; set; } // JSON

        // Datos de Sesión
        public DateTime? FechaUltimoLogin { get; set; }
        [MaxLength(255)]
        public string? TokenRecuperacion { get; set; }
        public DateTime? FechaExpiracionTokenRecuperacion { get; set; }
        [MaxLength(255)]
        public string? TokenVerificacionEmail { get; set; }
        public bool EmailVerificado { get; set; } = false;

        // Configuración Regional
        [Required]
        [MaxLength(10)]
        public string Idioma { get; set; } = "es-CL";

        [Required]
        [MaxLength(50)]
        public string ZonaHoraria { get; set; } = "America/Santiago";

        // Propiedades Calculadas
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public bool RequiereVerificacionEmail => !EmailVerificado;

        public bool TieneTokenRecuperacionValido =>
            !string.IsNullOrEmpty(TokenRecuperacion) &&
            FechaExpiracionTokenRecuperacion.HasValue &&
            FechaExpiracionTokenRecuperacion > DateTime.UtcNow;

        // Propiedades de Navegación
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
        public virtual ICollection<SesionUsuario> Sesiones { get; set; } = new List<SesionUsuario>();
        public virtual ICollection<AuditoriaAcceso> AuditoriasAcceso { get; set; } = new List<AuditoriaAcceso>();
    }
}
