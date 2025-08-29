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
    public class SesionUsuarioConfiguration : IEntityTypeConfiguration<SesionUsuario>
    {
        public void Configure(EntityTypeBuilder<SesionUsuario> builder)
        {
            // Configuración de tabla
            builder.ToTable("SesionesUsuario");
            builder.HasKey(s => s.Id);

            // Configuración de propiedades
            builder.Property(s => s.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(s => s.UsuarioId)
                .IsRequired();

            builder.Property(s => s.TokenSesion)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.FechaInicioSesion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.FechaExpiracion)
                .IsRequired();

            builder.Property(s => s.FechaUltimaActividad)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.DireccionIP)
                .IsRequired()
                .HasMaxLength(45); // IPv6 support

            builder.Property(s => s.UserAgent)
                .HasMaxLength(500);

            builder.Property(s => s.Dispositivo)
                .HasMaxLength(100);

            builder.Property(s => s.SistemaOperativo)
                .HasMaxLength(50);

            builder.Property(s => s.Navegador)
                .HasMaxLength(50);

            builder.Property(s => s.SesionActiva)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(s => s.RazonCierre)
                .HasMaxLength(50);

            // Propiedades calculadas ignoradas
            builder.Ignore(s => s.EstaExpirada);
            builder.Ignore(s => s.RequiereRenovacion);
            builder.Ignore(s => s.TiempoSesion);

            // Índices únicos
            builder.HasIndex(s => s.TokenSesion)
                .IsUnique()
                .HasDatabaseName("UK_SesionesUsuario_Token");

            // Índices de rendimiento
            builder.HasIndex(s => s.UsuarioId)
                .HasDatabaseName("IX_SesionesUsuario_UsuarioId");

            builder.HasIndex(s => s.SesionActiva)
                .HasDatabaseName("IX_SesionesUsuario_SesionActiva");

            builder.HasIndex(s => s.FechaExpiracion)
                .HasDatabaseName("IX_SesionesUsuario_FechaExpiracion");

            builder.HasIndex(s => s.DireccionIP)
                .HasDatabaseName("IX_SesionesUsuario_DireccionIP");

            // Relaciones
            builder.HasOne(s => s.Usuario)
                .WithMany(u => u.Sesiones)
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar tabla con restricciones CHECK
            builder.ToTable("SesionesUsuario", t =>
            {
                t.HasCheckConstraint("CK_SesionesUsuario_FechasValidas",
                    "[FechaExpiracion] > [FechaInicioSesion]");

                t.HasCheckConstraint("CK_SesionesUsuario_RazonCierre",
                    "([SesionActiva] = 1 AND [FechaCierre] IS NULL AND [RazonCierre] IS NULL) OR " +
                    "([SesionActiva] = 0 AND [FechaCierre] IS NOT NULL AND [RazonCierre] IS NOT NULL)");

                t.HasCheckConstraint("CK_SesionesUsuario_RazonCierreValores",
                    "[RazonCierre] IS NULL OR [RazonCierre] IN ('LOGOUT', 'TIMEOUT', 'FORCE_CLOSE', 'EXPIRED')");
            });
        }
    }
}
