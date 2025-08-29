
namespace NexaERP.Core.Entities.Base
{
    /// <summary>
    /// Interfaz para entidades que requieren auditoría de cambios
    /// Implementa el patrón de auditoría estándar del sistema
    /// </summary>
    public interface IAuditableEntity : IEntity
    {
        DateTime FechaCreacion { get; set; }
        DateTime? FechaModificacion { get; set; }
        int UsuarioCreacion { get; set; }
        int? UsuarioModificacion { get; set; }
        bool Activo { get; set; }
    }
}
