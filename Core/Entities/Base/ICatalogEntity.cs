
namespace NexaERP.Core.Entities.Base
{
    /// <summary>
    /// Interfaz para entidades de catálogo/configuración
    /// Entidades que definen tipos y configuraciones del sistema
    /// </summary>
    public interface ICatalogEntity : IEntity
    {
        string Codigo { get; set; }
        string Nombre { get; set; }
        string? Descripcion { get; set; }
        bool Activo { get; set; }
        DateTime FechaCreacion { get; set; }
        int Orden { get; set; }
    }
}
