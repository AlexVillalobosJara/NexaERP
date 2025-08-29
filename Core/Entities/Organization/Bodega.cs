using Core.Entities.Base;

namespace Core.Entities.Organization
{
    public class Bodega : BaseEntity
    {
        public int SucursalId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public virtual Sucursal Sucursal { get; set; } = null!;
    }
}
