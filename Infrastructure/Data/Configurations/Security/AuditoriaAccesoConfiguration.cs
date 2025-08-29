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
    public class AuditoriaAccesoConfiguration : IEntityTypeConfiguration<AuditoriaAcceso>
    {
        public void Configure(EntityTypeBuilder<AuditoriaAcceso> builder)
        {
            // Configuración de tabla
            builder.ToTable("AuditoriaAccesos");
            builder.HasKey(a => a.Id);

            // Configuración de propiedades
            builder.Property(a => a.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(a => a.Email)
                .HasMaxLength(100);

            builder.Property(a => a.TipoEvento)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(a => a.FechaEvento)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(a => a.DireccionIP)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(a => a.UserAgent)
                .HasMaxLength(500);

            builder.Property(a => a.Exitoso)
                .IsRequired();

            builder.Property(a => a.MensajeError)
                .HasMaxLength(300);

            builder.Property(a => a.DatosAdicionales)
                .HasColumnType("nvarchar(max)");

            // Propiedades calculadas ignoradas
            builder.Ignore(a => a.TipoEventoDescripcion);

            // Índices de rendimiento
            builder.HasIndex(a => a.UsuarioId)
                .HasDatabaseName("IX_AuditoriaAccesos_UsuarioId");

            builder.HasIndex(a => a.FechaEvento)
                .HasDatabaseName("IX_AuditoriaAccesos_FechaEvento");

            builder.HasIndex(a => a.TipoEvento)
                .HasDatabaseName("IX_AuditoriaAccesos_TipoEvento");

            builder.HasIndex(a => a.DireccionIP)
                .HasDatabaseName("IX_AuditoriaAccesos_DireccionIP");

            builder.HasIndex(a => a.Exitoso)
                .HasDatabaseName("IX_AuditoriaAccesos_Exitoso");

            // Índice compuesto para consultas comunes
            builder.HasIndex(a => new { a.FechaEvento, a.TipoEvento, a.Exitoso })
                .HasDatabaseName("IX_AuditoriaAccesos_FechaTipoExitoso");

            // Relaciones
            builder.HasOne(a => a.Usuario)
                .WithMany(u => u.AuditoriasAcceso)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
