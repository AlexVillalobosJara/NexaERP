using Core.Entities.Organization;
using NexaERP.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Security
{
    /// <summary>
    /// Entidad Rol - Sistema de roles multi-tenant
    /// Roles configurables por empresa con permisos granulares
    /// </summary>
    public class Rol : TenantEntity
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        // Configuración del Rol
        public bool EsRolSistema { get; set; } = false;

        // Permisos Globales del Rol
        public bool EsAdministrador { get; set; } = false;
        public bool PuedeGestionarUsuarios { get; set; } = false;
        public bool PuedeGestionarRoles { get; set; } = false;
        public bool PuedeVerTodosLosDatos { get; set; } = false;

        // Propiedades de Navegación
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
