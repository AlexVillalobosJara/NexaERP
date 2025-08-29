using Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace NexaERP.Core.Entities.Base
{
    /// <summary>
    /// Entidad base para catálogos del sistema
    /// Para tablas de configuración como TiposMoneda, TiposSucursal, etc.
    /// </summary>
    public abstract class CatalogEntity : BaseEntity, ICatalogEntity
    {
        [Required]
        [MaxLength(50)]
        public virtual string Codigo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public virtual string Nombre { get; set; } = string.Empty;

        [MaxLength(500)]
        public virtual string? Descripcion { get; set; }

        [Required]
        public virtual bool Activo { get; set; } = true;

        [Required]
        public virtual DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Required]
        public virtual int Orden { get; set; } = 0;
    }
}
