using Core.Entities.Base;
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Security
{
    /// <summary>
    /// Entidad AuditoriaAcceso - Log completo de eventos de seguridad
    /// Cumple con requerimientos de auditoría y trazabilidad
    /// </summary>
    public class AuditoriaAcceso : BaseEntity
    {
        // Datos del Evento
        public int? UsuarioId { get; set; } // NULL si el login falló

        [MaxLength(100)]
        public string? Email { get; set; } // Para logins fallidos

        [Required]
        public TipoEvento TipoEvento { get; set; }

        // Detalles del Evento
        [Required]
        public DateTime FechaEvento { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(45)]
        public string DireccionIP { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? UserAgent { get; set; }

        [Required]
        public bool Exitoso { get; set; }

        [MaxLength(300)]
        public string? MensajeError { get; set; }

        // Información Adicional (JSON)
        public string? DatosAdicionales { get; set; }

        // Propiedades Calculadas
        public string TipoEventoDescripcion => TipoEvento switch
        {
            TipoEvento.LoginSuccess => "Inicio de sesión exitoso",
            TipoEvento.LoginFailed => "Intento de inicio de sesión fallido",
            TipoEvento.Logout => "Cierre de sesión",
            TipoEvento.PasswordChange => "Cambio de contraseña",
            TipoEvento.PasswordReset => "Restablecimiento de contraseña",
            TipoEvento.AccountLocked => "Cuenta bloqueada",
            TipoEvento.AccountUnlocked => "Cuenta desbloqueada",
            TipoEvento.TwoFactorEnabled => "2FA habilitado",
            TipoEvento.TwoFactorDisabled => "2FA deshabilitado",
            TipoEvento.EmailVerified => "Email verificado",
            TipoEvento.SessionForceClose => "Sesión cerrada forzosamente",
            TipoEvento.UserCreated => "Usuario creado",
            TipoEvento.UserModified => "Usuario modificado",
            TipoEvento.RoleAssigned => "Rol asignado",
            TipoEvento.RoleRevoked => "Rol revocado",
            TipoEvento.PermissionGranted => "Permiso concedido",
            TipoEvento.PermissionDenied => "Permiso denegado",
            _ => "Evento desconocido"
        };

        // Propiedades de Navegación
        public virtual Usuario? Usuario { get; set; }
    }
}
