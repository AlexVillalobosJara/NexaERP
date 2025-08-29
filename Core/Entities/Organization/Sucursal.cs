using NexaERP.Core.Entities.Base;

namespace Core.Entities.Organization
{
    public class Sucursal : TenantEntity
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual ICollection<Bodega> Bodegas { get; set; } = new List<Bodega>();
    }
}
