using Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace NexaERP.Core.Entities.Base
{
    /// <summary>
    /// Entidad base para multi-tenancy
    /// Incluye EmpresaId para separación de datos por empresa
    /// </summary>
    public abstract class TenantEntity : AuditableEntity, ITenantEntity
    {
        [Required]
        public virtual int EmpresaId { get; set; }

        // Propiedad de navegación que se configurará en cada entidad específica
        // public virtual Empresa Empresa { get; set; } = null!;
    }
}
