using Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Security
{
    /// <summary>
    /// Entidad Permiso - Permisos granulares del sistema
    /// Permisos organizados por módulos y categorías según especificación
    /// </summary>
    public class Permiso : BaseEntity
    {
        // Identificación del Permiso
        [Required]
        [MaxLength(50)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        // Categorización
        [Required]
        [MaxLength(30)]
        public string Modulo { get; set; } = string.Empty; // "INVENTARIO", "VENTAS", "COMPRAS"

        [Required]
        [MaxLength(30)]
        public string Categoria { get; set; } = string.Empty; // "CREAR", "EDITAR", "ELIMINAR", "CONSULTAR"

        // Configuración
        public bool EsPermisoSistema { get; set; } = true;
        public bool Activo { get; set; } = true;

        // Propiedades de Navegación
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
