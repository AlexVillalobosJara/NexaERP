using NexaERP.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Base
{
    /// <summary>
    /// Interfaz para entidades multi-tenant
    /// Garantiza que toda entidad principal esté asociada a una empresa
    /// </summary>
    public interface ITenantEntity : IAuditableEntity
    {
        int EmpresaId { get; set; }
    }
}
