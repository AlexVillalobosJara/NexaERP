using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations.Security
{
    public class RolPermisoConfiguration : IEntityTypeConfiguration<RolPermiso>
    {
        public void Configure(EntityTypeBuilder<RolPermiso> builder)
        {
            // Configuración de tabla
            builder.ToTable("RolesPermisos");
            builder.HasKey(rp => rp.Id);

            // Configuración de propiedades
            builder.Property(rp => rp.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(rp => rp.RolId)
                .IsRequired();

            builder.Property(rp => rp.PermisoId)
                .IsRequired();

            builder.Property(rp => rp.Concedido)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(rp => rp.FechaAsignacion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(rp => rp.UsuarioAsignacion)
                .IsRequired();

            // Índice único compuesto
            builder.HasIndex(rp => new { rp.RolId, rp.PermisoId })
                .IsUnique()
                .HasDatabaseName("UK_RolesPermisos_RolPermiso");

            // Índices de rendimiento
            builder.HasIndex(rp => rp.RolId)
                .HasDatabaseName("IX_RolesPermisos_RolId");

            builder.HasIndex(rp => rp.PermisoId)
                .HasDatabaseName("IX_RolesPermisos_PermisoId");

            // Relaciones
            builder.HasOne(rp => rp.Rol)
                .WithMany(r => r.RolPermisos)
                .HasForeignKey(rp => rp.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rp => rp.Permiso)
                .WithMany(p => p.RolPermisos)
                .HasForeignKey(rp => rp.PermisoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
