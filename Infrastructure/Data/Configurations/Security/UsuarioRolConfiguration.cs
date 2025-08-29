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
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
    {
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            // Configuración de tabla
            builder.ToTable("UsuariosRoles");
            builder.HasKey(ur => ur.Id);

            // Configuración de propiedades
            builder.Property(ur => ur.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(ur => ur.UsuarioId)
                .IsRequired();

            builder.Property(ur => ur.RolId)
                .IsRequired();

            builder.Property(ur => ur.FechaAsignacion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ur => ur.UsuarioAsignacion)
                .IsRequired();

            builder.Property(ur => ur.Activo)
                .IsRequired()
                .HasDefaultValue(true);

            // Propiedades calculadas ignoradas
            builder.Ignore(ur => ur.EstaVigente);
            builder.Ignore(ur => ur.TieneRestriccionSucursal);
            builder.Ignore(ur => ur.TieneRestriccionBodega);

            // Índice único compuesto
            builder.HasIndex(ur => new { ur.UsuarioId, ur.RolId, ur.SucursalId, ur.BodegaId })
                .IsUnique()
                .HasDatabaseName("UK_UsuariosRoles_UsuarioRolSucursalBodega");

            // Índices de rendimiento
            builder.HasIndex(ur => ur.UsuarioId)
                .HasDatabaseName("IX_UsuariosRoles_UsuarioId");

            builder.HasIndex(ur => ur.RolId)
                .HasDatabaseName("IX_UsuariosRoles_RolId");

            builder.HasIndex(ur => ur.Activo)
                .HasDatabaseName("IX_UsuariosRoles_Activo");

            builder.HasIndex(ur => ur.FechaVencimiento)
                .HasDatabaseName("IX_UsuariosRoles_Vigencia")
                .HasFilter("[FechaVencimiento] IS NOT NULL");

            // Relaciones
            builder.HasOne(ur => ur.Usuario)
                .WithMany(u => u.UsuarioRoles)
                .HasForeignKey(ur => ur.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Rol)
                .WithMany(r => r.UsuarioRoles)
                .HasForeignKey(ur => ur.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Sucursal)
                .WithMany()
                .HasForeignKey(ur => ur.SucursalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Bodega)
                .WithMany()
                .HasForeignKey(ur => ur.BodegaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar tabla con restricciones CHECK
            builder.ToTable("UsuariosRoles", t =>
            {
                t.HasCheckConstraint("CK_UsuariosRoles_VencimientoValido",
                    "[FechaVencimiento] IS NULL OR [FechaVencimiento] > [FechaAsignacion]");
            });
        }
    }
}
