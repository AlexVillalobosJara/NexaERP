
namespace Core.Enums
{
    /// <summary>
    /// Tipos de eventos para auditoría del sistema
    /// Basado en los eventos definidos en la especificación técnica
    /// </summary>
    public enum TipoEvento
    {
        LoginSuccess = 1,
        LoginFailed = 2,
        Logout = 3,
        PasswordChange = 4,
        PasswordReset = 5,
        AccountLocked = 6,
        AccountUnlocked = 7,
        TwoFactorEnabled = 8,
        TwoFactorDisabled = 9,
        EmailVerified = 10,
        SessionForceClose = 11,
        UserCreated = 12,
        UserModified = 13,
        RoleAssigned = 14,
        RoleRevoked = 15,
        PermissionGranted = 16,
        PermissionDenied = 17
    }
}
