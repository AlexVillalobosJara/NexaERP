
namespace Core.DTOs.Security
{
    public class AuthenticationResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public UsuarioDTO? User { get; set; }
        public string? ErrorMessage { get; set; }
        public bool RequiresTwoFactor { get; set; } = false;
        public bool IsAccountLocked { get; set; } = false;
        public bool RequiresPasswordChange { get; set; } = false;
    }
}
