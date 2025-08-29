using Core.Entities.Base;
using NexaERP.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    /// <summary>
    /// Extensiones útiles para las entidades base
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Marca una entidad como modificada, actualizando los campos de auditoría
        /// </summary>
        /// <param name="entity">Entidad a modificar</param>
        /// <param name="usuarioId">ID del usuario que realiza la modificación</param>
        public static T MarkAsModified<T>(this T entity, int usuarioId) where T : IAuditableEntity
        {
            entity.FechaModificacion = DateTime.UtcNow;
            entity.UsuarioModificacion = usuarioId;
            return entity;
        }

        /// <summary>
        /// Marca una entidad como eliminada (soft delete)
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        /// <param name="usuarioId">ID del usuario que realiza la eliminación</param>
        public static T MarkAsDeleted<T>(this T entity, int usuarioId) where T : IAuditableEntity
        {
            entity.Activo = false;
            entity.FechaModificacion = DateTime.UtcNow;
            entity.UsuarioModificacion = usuarioId;
            return entity;
        }

        /// <summary>
        /// Verifica si una entidad está activa
        /// </summary>
        /// <param name="entity">Entidad a verificar</param>
        /// <returns>True si la entidad está activa</returns>
        public static bool IsActive<T>(this T entity) where T : IAuditableEntity
        {
            return entity.Activo;
        }

        /// <summary>
        /// Verifica si una entidad fue creada por el usuario especificado
        /// </summary>
        /// <param name="entity">Entidad a verificar</param>
        /// <param name="usuarioId">ID del usuario</param>
        /// <returns>True si fue creada por el usuario</returns>
        public static bool WasCreatedBy<T>(this T entity, int usuarioId) where T : IAuditableEntity
        {
            return entity.UsuarioCreacion == usuarioId;
        }

        /// <summary>
        /// Verifica si una entidad pertenece a la empresa especificada
        /// </summary>
        /// <param name="entity">Entidad a verificar</param>
        /// <param name="empresaId">ID de la empresa</param>
        /// <returns>True si pertenece a la empresa</returns>
        public static bool BelongsToCompany<T>(this T entity, int empresaId) where T : ITenantEntity
        {
            return entity.EmpresaId == empresaId;
        }
    }
}
