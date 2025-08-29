using Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Security
{
    /// <summary>
    /// Entidad RolPermiso - Relación many-to-many entre Rol y Permiso
    /// Permite conceder o denegar permisos explícitamente
    /// </summary>
    public class RolPermiso : BaseEntity
    {
        [Required]
        public int RolId { get; set; }

        [Required]
        public int PermisoId { get; set; }

        // Configuración del Permiso
        [Required]
        public bool Concedido { get; set; } = true;

        // Auditoría
        [Required]
        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

        [Required]
        public int UsuarioAsignacion { get; set; }

        // Propiedades de Navegación
        public virtual Rol Rol { get; set; } = null!;
        public virtual Permiso Permiso { get; set; } = null!;
    }
}
