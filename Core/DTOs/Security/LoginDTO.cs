using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Security;

public class LoginDTO
{
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; } = false;

    public string? TwoFactorCode { get; set; }
}
