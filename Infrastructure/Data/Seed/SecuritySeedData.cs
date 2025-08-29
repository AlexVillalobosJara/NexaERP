using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using NexaERP.Infrastructure.Data.Context;

namespace Infrastructure.Data.Seed
{
    /// <summary>
    /// Datos semilla para el sistema de seguridad
    /// Crea permisos del sistema y roles predefinidos
    /// </summary>
    public static class SecuritySeedData
    {
        /// <summary>
        /// Aplicar datos semilla de seguridad
        /// </summary>
        public static async Task SeedAsync(NexaErpDbContext context)
        {
            // Seed Permisos del Sistema
            await SeedPermisos(context);

            // Los roles se crean por empresa, no aquí
            // await SeedRoles(context);
        }

        /// <summary>
        /// Crear permisos del sistema
        /// </summary>
        private static async Task SeedPermisos(NexaErpDbContext context)
        {
            var permisos = new List<Permiso>
            {
                // Permisos de Sistema
                new() { Codigo = "SISTEMA_ADMINISTRAR", Nombre = "Administrar Sistema", Modulo = "SISTEMA", Categoria = "ADMINISTRAR" },
                new() { Codigo = "SISTEMA_CONFIGURAR", Nombre = "Configurar Sistema", Modulo = "SISTEMA", Categoria = "CONFIGURAR" },

                // Permisos de Usuarios
                new() { Codigo = "USUARIO_CONSULTAR", Nombre = "Consultar Usuarios", Modulo = "USUARIO", Categoria = "CONSULTAR" },
                new() { Codigo = "USUARIO_CREAR", Nombre = "Crear Usuario", Modulo = "USUARIO", Categoria = "CREAR" },
                new() { Codigo = "USUARIO_EDITAR", Nombre = "Editar Usuario", Modulo = "USUARIO", Categoria = "EDITAR" },
                new() { Codigo = "USUARIO_ELIMINAR", Nombre = "Eliminar Usuario", Modulo = "USUARIO", Categoria = "ELIMINAR" },
                new() { Codigo = "USUARIO_GESTIONAR", Nombre = "Gestionar Usuarios", Modulo = "USUARIO", Categoria = "GESTIONAR" },

                // Permisos de Roles
                new() { Codigo = "ROL_CONSULTAR", Nombre = "Consultar Roles", Modulo = "ROL", Categoria = "CONSULTAR" },
                new() { Codigo = "ROL_CREAR", Nombre = "Crear Rol", Modulo = "ROL", Categoria = "CREAR" },
                new() { Codigo = "ROL_EDITAR", Nombre = "Editar Rol", Modulo = "ROL", Categoria = "EDITAR" },
                new() { Codigo = "ROL_ELIMINAR", Nombre = "Eliminar Rol", Modulo = "ROL", Categoria = "ELIMINAR" },
                new() { Codigo = "ROL_GESTIONAR", Nombre = "Gestionar Roles", Modulo = "ROL", Categoria = "GESTIONAR" },

                // Permisos de Inventario
                new() { Codigo = "INVENTARIO_CONSULTAR", Nombre = "Consultar Inventario", Modulo = "INVENTARIO", Categoria = "CONSULTAR" },
                new() { Codigo = "INVENTARIO_CREAR", Nombre = "Crear Productos", Modulo = "INVENTARIO", Categoria = "CREAR" },
                new() { Codigo = "INVENTARIO_EDITAR", Nombre = "Editar Productos", Modulo = "INVENTARIO", Categoria = "EDITAR" },
                new() { Codigo = "INVENTARIO_ELIMINAR", Nombre = "Eliminar Productos", Modulo = "INVENTARIO", Categoria = "ELIMINAR" },
                new() { Codigo = "INVENTARIO_MOVIMIENTOS", Nombre = "Realizar Movimientos", Modulo = "INVENTARIO", Categoria = "MOVIMIENTOS" },
                new() { Codigo = "INVENTARIO_TRANSFERENCIAS", Nombre = "Transferir Stock", Modulo = "INVENTARIO", Categoria = "TRANSFERENCIAS" },
                new() { Codigo = "INVENTARIO_AJUSTES", Nombre = "Ajustes de Inventario", Modulo = "INVENTARIO", Categoria = "AJUSTES" },
                new() { Codigo = "INVENTARIO_REPORTES", Nombre = "Reportes de Inventario", Modulo = "INVENTARIO", Categoria = "REPORTES" },

                // Permisos de Empresa (Multi-tenant)
                new() { Codigo = "EMPRESA_CONSULTAR", Nombre = "Consultar Empresas", Modulo = "EMPRESA", Categoria = "CONSULTAR" },
                new() { Codigo = "EMPRESA_CREAR", Nombre = "Crear Empresa", Modulo = "EMPRESA", Categoria = "CREAR" },
                new() { Codigo = "EMPRESA_EDITAR", Nombre = "Editar Empresa", Modulo = "EMPRESA", Categoria = "EDITAR" },
                new() { Codigo = "EMPRESA_CONFIGURAR", Nombre = "Configurar Empresa", Modulo = "EMPRESA", Categoria = "CONFIGURAR" }
            };

            foreach (var permiso in permisos)
            {
                if (!await context.Permisos.AnyAsync(p => p.Codigo == permiso.Codigo))
                {
                    context.Permisos.Add(permiso);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
