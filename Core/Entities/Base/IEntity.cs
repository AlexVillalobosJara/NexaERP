
namespace NexaERP.Core.Entities.Base
{
    /// <summary>
    /// Interfaz base para todas las entidades del sistema
    /// Garantiza que toda entidad tenga un identificador único
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
    }
}
