using Core.DTOs.Security;

namespace Core.Interfaces.Services.Security
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResultDTO> LoginAsync(LoginDTO loginRequest);
        Task<AuthenticationResultDTO> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(int usuarioId, string tokenSesion);
        Task<bool> ValidateTokenAsync(string token);
        Task<UsuarioDTO?> GetCurrentUserAsync(string token);
    }
}
