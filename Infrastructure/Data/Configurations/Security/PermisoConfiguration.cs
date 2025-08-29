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
    public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            // Configuración de tabla
            builder.ToTable("Permisos");
            builder.HasKey(p => p.Id);

            // Configuración de propiedades
            builder.Property(p => p.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(p => p.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(200);

            builder.Property(p => p.Modulo)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Categoria)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.EsPermisoSistema)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.Activo)
                .IsRequired()
                .HasDefaultValue(true);

            // Índices únicos
            builder.HasIndex(p => p.Codigo)
                .IsUnique()
                .HasDatabaseName("UK_Permisos_Codigo");

            // Índices de rendimiento
            builder.HasIndex(p => p.Modulo)
                .HasDatabaseName("IX_Permisos_Modulo");

            builder.HasIndex(p => p.Categoria)
                .HasDatabaseName("IX_Permisos_Categoria");

            builder.HasIndex(p => p.Activo)
                .HasDatabaseName("IX_Permisos_Activo");
        }
    }
}
