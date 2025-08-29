using Core.Entities.Base;
using Core.Entities.Organization;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Security
{
    /// <summary>
    /// Entidad UsuarioRol - Relación many-to-many entre Usuario y Rol
    /// Permite asignación temporal y restricciones por sucursal/bodega
    /// </summary>
    public class UsuarioRol : BaseEntity
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int RolId { get; set; }

        // Vigencia del Rol
        [Required]
        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaVencimiento { get; set; }

        // Restricciones Adicionales (opcional)
        public int? SucursalId { get; set; }
        public int? BodegaId { get; set; }

        // Auditoría
        [Required]
        public int UsuarioAsignacion { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        // Propiedades Calculadas
        public bool EstaVigente =>
            Activo && (FechaVencimiento == null || FechaVencimiento > DateTime.UtcNow);

        public bool TieneRestriccionSucursal => SucursalId.HasValue;
        public bool TieneRestriccionBodega => BodegaId.HasValue;

        // Propiedades de Navegación
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Rol Rol { get; set; } = null!;
        public virtual Sucursal? Sucursal { get; set; }
        public virtual Bodega? Bodega { get; set; }
    }
}
