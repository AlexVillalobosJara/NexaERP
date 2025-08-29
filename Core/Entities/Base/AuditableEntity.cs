using Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace NexaERP.Core.Entities.Base
{
    /// <summary>
    /// Entidad base auditable que implementa IAuditableEntity
    /// Incluye campos estándar de auditoría para todas las entidades principales
    /// </summary>
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity
    {
        [Required]
        public virtual DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public virtual DateTime? FechaModificacion { get; set; }

        [Required]
        public virtual int UsuarioCreacion { get; set; }

        public virtual int? UsuarioModificacion { get; set; }

        [Required]
        public virtual bool Activo { get; set; } = true;
    }
}