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
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            // Configuración de tabla
            builder.ToTable("Roles");
            builder.HasKey(r => r.Id);

            // Configuración de propiedades
            builder.Property(r => r.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(r => r.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Descripcion)
                .HasMaxLength(200);

            builder.Property(r => r.EsRolSistema)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.EsAdministrador)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.PuedeGestionarUsuarios)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.PuedeGestionarRoles)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.PuedeVerTodosLosDatos)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.FechaCreacion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(r => r.Activo)
                .IsRequired()
                .HasDefaultValue(true);

            // Índices únicos
            builder.HasIndex(r => new { r.EmpresaId, r.Nombre })
                .IsUnique()
                .HasDatabaseName("UK_Roles_EmpresaNombre");

            // Índices de rendimiento
            builder.HasIndex(r => r.EmpresaId)
                .HasDatabaseName("IX_Roles_EmpresaId");

            builder.HasIndex(r => r.Activo)
                .HasDatabaseName("IX_Roles_Activo");

            builder.HasIndex(r => r.EsRolSistema)
                .HasDatabaseName("IX_Roles_EsRolSistema")
                .HasFilter("[EsRolSistema] = 1");

            // Relaciones
            builder.HasOne(r => r.Empresa)
                .WithMany(e => e.Roles)
                .HasForeignKey(r => r.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
