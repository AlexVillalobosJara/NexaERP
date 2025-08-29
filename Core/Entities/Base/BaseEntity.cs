using NexaERP.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Base
{
    /// <summary>
    /// Entidad base que implementa IEntity
    /// Clase base para entidades simples sin auditoría
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
