using Core.Entities.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Organization
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            // Configuración de tabla
            builder.ToTable("Empresas");
            builder.HasKey(e => e.Id);

            // Configuración de propiedades
            builder.Property(e => e.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(e => e.RutEmpresa)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(e => e.RazonSocial)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.NombreComercial)
                .HasMaxLength(150);

            builder.Property(e => e.Giro)
                .HasMaxLength(200);

            // Dirección Casa Matriz
            builder.Property(e => e.DireccionCasaMatriz)
                .HasMaxLength(300);

            builder.Property(e => e.ComunaCasaMatriz)
                .HasMaxLength(100);

            builder.Property(e => e.CiudadCasaMatriz)
                .HasMaxLength(100);

            builder.Property(e => e.RegionCasaMatriz)
                .HasMaxLength(100);

            // Contacto
            builder.Property(e => e.Telefono)
                .HasMaxLength(15);

            builder.Property(e => e.Email)
                .HasMaxLength(100);

            builder.Property(e => e.SitioWeb)
                .HasMaxLength(150);

            // Control SII
            builder.Property(e => e.CodigoSII)
                .HasMaxLength(20);

            builder.Property(e => e.ResolucionSII)
                .HasMaxLength(100);

            // Configuración del Sistema
            builder.Property(e => e.MonedaPrincipalId)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.FormatoFecha)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("dd/MM/yyyy");

            builder.Property(e => e.SeparadorDecimal)
                .IsRequired()
                .HasMaxLength(1)
                .HasDefaultValue(",");

            builder.Property(e => e.MetodoValorizacionId)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.TiempoExpiracionSesion)
                .IsRequired()
                .HasDefaultValue(480);

            builder.Property(e => e.MaximoIntentosLogin)
                .IsRequired()
                .HasDefaultValue(5);

            builder.Property(e => e.TiempoBloqueoTemporalMinutos)
                .IsRequired()
                .HasDefaultValue(30);

            builder.Property(e => e.FechaCreacion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValue(true);

            // Índices únicos
            builder.HasIndex(e => e.RutEmpresa)
                .IsUnique()
                .HasDatabaseName("UK_Empresas_RutEmpresa");

            // Índices de rendimiento
            builder.HasIndex(e => e.RazonSocial)
                .HasDatabaseName("IX_Empresas_RazonSocial");

            builder.HasIndex(e => e.Activo)
                .HasDatabaseName("IX_Empresas_Activo");

            builder.HasIndex(e => e.FechaCreacion)
                .HasDatabaseName("IX_Empresas_FechaCreacion");

            // Configurar tabla con restricciones CHECK
            builder.ToTable("Empresas", t =>
            {
                t.HasCheckConstraint("CK_Empresas_SeparadorDecimal",
                    "[SeparadorDecimal] IN (',', '.')");

                t.HasCheckConstraint("CK_Empresas_TiempoExpiracion",
                    "[TiempoExpiracionSesion] BETWEEN 30 AND 1440");

                t.HasCheckConstraint("CK_Empresas_MaximoIntentos",
                    "[MaximoIntentosLogin] BETWEEN 3 AND 10");

                t.HasCheckConstraint("CK_Empresas_TiempoBloqueo",
                    "[TiempoBloqueoTemporalMinutos] > 0");
            });
        }
    }
}
