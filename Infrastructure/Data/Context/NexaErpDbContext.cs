using Core.Entities.Base;
using Core.Entities.Organization;
using Core.Entities.Security;
using Infrastructure.Data.Configurations.Organization;
using Infrastructure.Data.Configurations.Security;
using Microsoft.EntityFrameworkCore;
using NexaERP.Core.Entities.Base;

namespace NexaERP.Infrastructure.Data.Context
{
    public class NexaErpDbContext : DbContext
    {
        public NexaErpDbContext(DbContextOptions<NexaErpDbContext> options) : base(options)
        {
        }

        // DbSets de Seguridad
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Permiso> Permisos { get; set; } = null!;
        public DbSet<UsuarioRol> UsuariosRoles { get; set; } = null!;
        public DbSet<RolPermiso> RolesPermisos { get; set; } = null!;
        public DbSet<SesionUsuario> SesionesUsuario { get; set; } = null!;
        public DbSet<AuditoriaAcceso> AuditoriaAccesos { get; set; } = null!;

        // DbSets de Organización
        public DbSet<Empresa> Empresas { get; set; } = null!;
        public DbSet<Sucursal> Sucursales { get; set; } = null!;
        public DbSet<Bodega> Bodegas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar configuraciones de Seguridad
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new RolConfiguration());
            modelBuilder.ApplyConfiguration(new PermisoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioRolConfiguration());
            modelBuilder.ApplyConfiguration(new RolPermisoConfiguration());
            modelBuilder.ApplyConfiguration(new SesionUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new AuditoriaAccesoConfiguration());

            // Aplicar configuraciones de Organización
            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());

            // TODO: Aplicar configuraciones de Catálogos cuando se implementen
            // modelBuilder.ApplyConfiguration(new TipoMonedaConfiguration());
            // modelBuilder.ApplyConfiguration(new TipoSucursalConfiguration());

            // Configuración global para entidades auditables
            ConfigureAuditableEntities(modelBuilder);

            // Configuración global para entidades multi-tenant
            ConfigureTenantEntities(modelBuilder);
        }

        /// <summary>
        /// Configuración global para entidades auditables
        /// </summary>
        private static void ConfigureAuditableEntities(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                if (typeof(IAuditableEntity).IsAssignableFrom(clrType))
                {
                    // Configurar valores por defecto para entidades auditables
                    modelBuilder.Entity(clrType)
                        .Property("FechaCreacion")
                        .HasDefaultValueSql("GETUTCDATE()");

                    modelBuilder.Entity(clrType)
                        .Property("Activo")
                        .HasDefaultValue(true);
                }
            }
        }

        /// <summary>
        /// Configuración global para entidades multi-tenant
        /// </summary>
        private static void ConfigureTenantEntities(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                if (typeof(ITenantEntity).IsAssignableFrom(clrType))
                {
                    // Configurar índice para EmpresaId en todas las entidades tenant
                    modelBuilder.Entity(clrType)
                        .HasIndex("EmpresaId")
                        .HasDatabaseName($"IX_{entityType.GetTableName()}_EmpresaId");
                }
            }
        }

        /// <summary>
        /// Intercepta SaveChanges para aplicar auditoría automática
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInformation();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Intercepta SaveChanges para aplicar auditoría automática
        /// </summary>
        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }

        /// <summary>
        /// Aplica información de auditoría automáticamente
        /// </summary>
        private void ApplyAuditInformation()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditableEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var auditableEntity = (IAuditableEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    auditableEntity.FechaCreacion = DateTime.UtcNow;
                    // UsuarioCreacion debe ser establecido por el servicio de aplicación
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    auditableEntity.FechaModificacion = DateTime.UtcNow;
                    // UsuarioModificacion debe ser establecido por el servicio de aplicación
                }
            }
        }
    }
}