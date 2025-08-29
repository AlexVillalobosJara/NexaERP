using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    /// <summary>
    /// Tipos de permisos del sistema
    /// Organizado por módulos según la especificación
    /// </summary>
    public enum TipoPermiso
    {
        // Permisos de Sistema
        SistemaAdministrar = 1,
        SistemaConfigurar = 2,

        // Permisos de Usuarios
        UsuarioConsultar = 10,
        UsuarioCrear = 11,
        UsuarioEditar = 12,
        UsuarioEliminar = 13,
        UsuarioGestionar = 14,

        // Permisos de Roles
        RolConsultar = 20,
        RolCrear = 21,
        RolEditar = 22,
        RolEliminar = 23,
        RolGestionar = 24,

        // Permisos de Inventario (según especificación)
        InventarioConsultar = 100,
        InventarioCrear = 101,
        InventarioEditar = 102,
        InventarioEliminar = 103,
        InventarioMovimientos = 104,
        InventarioTransferencias = 105,
        InventarioAjustes = 106,
        InventarioReportes = 107
    }
}
