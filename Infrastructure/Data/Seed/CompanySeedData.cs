using Core.Constants;
using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using NexaERP.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    /// <summary>
    /// Datos semilla específicos por empresa
    /// Crea roles predefinidos y usuario administrador por empresa
    /// </summary>
    public static class CompanySeedData
    {
        /// <summary>
        /// Crear roles predefinidos para una empresa
        /// </summary>
        public static async Task SeedCompanyRoles(NexaErpDbContext context, int empresaId, int usuarioCreacion = 1)
        {
            var roles = new List<Rol>
            {
                new()
                {
                    EmpresaId = empresaId,
                    Nombre = SecurityConstants.AdminRole,
                    Descripcion = "Administrador del sistema con acceso completo",
                    EsRolSistema = true,
                    EsAdministrador = true,
                    PuedeGestionarUsuarios = true,
                    PuedeGestionarRoles = true,
                    PuedeVerTodosLosDatos = true,
                    UsuarioCreacion = usuarioCreacion
                },
                new()
                {
                    EmpresaId = empresaId,
                    Nombre = SecurityConstants.SupervisorInventarioRole,
                    Descripcion = "Supervisor del módulo de inventario",
                    EsRolSistema = true,
                    EsAdministrador = false,
                    PuedeGestionarUsuarios = false,
                    PuedeGestionarRoles = false,
                    PuedeVerTodosLosDatos = false,
                    UsuarioCreacion = usuarioCreacion
                },
                new()
                {
                    EmpresaId = empresaId,
                    Nombre = SecurityConstants.OperadorBodegaRole,
                    Descripcion = "Operador de bodega con permisos básicos",
                    EsRolSistema = true,
                    EsAdministrador = false,
                    PuedeGestionarUsuarios = false,
                    PuedeGestionarRoles = false,
                    PuedeVerTodosLosDatos = false,
                    UsuarioCreacion = usuarioCreacion
                },
                new()
                {
                    EmpresaId = empresaId,
                    Nombre = SecurityConstants.ConsultaRole,
                    Descripcion = "Solo lectura de información",
                    EsRolSistema = true,
                    EsAdministrador = false,
                    PuedeGestionarUsuarios = false,
                    PuedeGestionarRoles = false,
                    PuedeVerTodosLosDatos = false,
                    UsuarioCreacion = usuarioCreacion
                }
            };

            foreach (var rol in roles)
            {
                if (!await context.Roles.AnyAsync(r => r.EmpresaId == empresaId && r.Nombre == rol.Nombre))
                {
                    context.Roles.Add(rol);
                }
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Asignar permisos a roles del sistema
        /// </summary>
        public static async Task AssignRolePermissions(NexaErpDbContext context, int empresaId, int usuarioAsignacion = 1)
        {
            // Obtener roles de la empresa
            var adminRole = await context.Roles
                .FirstOrDefaultAsync(r => r.EmpresaId == empresaId && r.Nombre == SecurityConstants.AdminRole);

            var supervisorRole = await context.Roles
                .FirstOrDefaultAsync(r => r.EmpresaId == empresaId && r.Nombre == SecurityConstants.SupervisorInventarioRole);

            var operatorRole = await context.Roles
                .FirstOrDefaultAsync(r => r.EmpresaId == empresaId && r.Nombre == SecurityConstants.OperadorBodegaRole);

            var consultaRole = await context.Roles
                .FirstOrDefaultAsync(r => r.EmpresaId == empresaId && r.Nombre == SecurityConstants.ConsultaRole);

            // Obtener todos los permisos
            var todosPermisos = await context.Permisos.ToListAsync();
            var permisosInventario = todosPermisos.Where(p => p.Modulo == "INVENTARIO").ToList();
            var permisosConsulta = todosPermisos.Where(p => p.Categoria == "CONSULTAR").ToList();

            // Asignar permisos al Administrador (todos los permisos)
            if (adminRole != null)
            {
                await AsignarPermisosARol(context, adminRole.Id, todosPermisos, usuarioAsignacion);
            }

            // Asignar permisos al Supervisor de Inventario
            if (supervisorRole != null)
            {
                await AsignarPermisosARol(context, supervisorRole.Id, permisosInventario, usuarioAsignacion);
            }

            // Asignar permisos al Operador de Bodega (solo movimientos y transferencias)
            if (operatorRole != null)
            {
                var permisosOperador = permisosInventario
                    .Where(p => p.Categoria is "CONSULTAR" or "MOVIMIENTOS" or "TRANSFERENCIAS")
                    .ToList();
                await AsignarPermisosARol(context, operatorRole.Id, permisosOperador, usuarioAsignacion);
            }

            // Asignar permisos al rol Consulta (solo lectura)
            if (consultaRole != null)
            {
                await AsignarPermisosARol(context, consultaRole.Id, permisosConsulta, usuarioAsignacion);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Asignar lista de permisos a un rol
        /// </summary>
        private static async Task AsignarPermisosARol(NexaErpDbContext context, int rolId,
            IEnumerable<Permiso> permisos, int usuarioAsignacion)
        {
            foreach (var permiso in permisos)
            {
                if (!await context.RolesPermisos.AnyAsync(rp => rp.RolId == rolId && rp.PermisoId == permiso.Id))
                {
                    context.RolesPermisos.Add(new RolPermiso
                    {
                        RolId = rolId,
                        PermisoId = permiso.Id,
                        Concedido = true,
                        UsuarioAsignacion = usuarioAsignacion
                    });
                }
            }
        }
    }
}
