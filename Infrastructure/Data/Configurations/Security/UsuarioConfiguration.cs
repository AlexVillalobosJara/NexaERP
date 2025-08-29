using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Security
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Configurar tabla con restricciones CHECK
            builder.ToTable("Usuarios", t =>
            {
                t.HasCheckConstraint("CK_Usuarios_IntentosLogin",
                    "[IntentosLoginFallidos] >= 0 AND [IntentosLoginFallidos] <= 10");

                t.HasCheckConstraint("CK_Usuarios_Idioma",
                    "[Idioma] IN ('es-CL', 'es-ES', 'en-US')");
            });

            builder.HasKey(u => u.Id);

            // Configuración de propiedades
            builder.Property(u => u.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(u => u.NombreUsuario)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Salt)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Nombres)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.RutPersona)
                .HasMaxLength(12);

            builder.Property(u => u.SecretoTOTP)
                .HasMaxLength(255);

            builder.Property(u => u.CodigosRecuperacion)
                .HasColumnType("nvarchar(max)");

            builder.Property(u => u.TokenRecuperacion)
                .HasMaxLength(255);

            builder.Property(u => u.TokenVerificacionEmail)
                .HasMaxLength(255);

            builder.Property(u => u.Idioma)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("es-CL");

            builder.Property(u => u.ZonaHoraria)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("America/Santiago");

            builder.Property(u => u.FechaCreacion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.Activo)
                .IsRequired()
                .HasDefaultValue(true);

            // Propiedades calculadas ignoradas
            builder.Ignore(u => u.NombreCompleto);
            builder.Ignore(u => u.RequiereVerificacionEmail);
            builder.Ignore(u => u.TieneTokenRecuperacionValido);

            // Índices únicos
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("UK_Usuarios_Email");

            builder.HasIndex(u => new { u.EmpresaId, u.NombreUsuario })
                .IsUnique()
                .HasDatabaseName("UK_Usuarios_EmpresaNombreUsuario");

            // Índices de rendimiento
            builder.HasIndex(u => u.EmpresaId)
                .HasDatabaseName("IX_Usuarios_EmpresaId");

            builder.HasIndex(u => u.Activo)
                .HasDatabaseName("IX_Usuarios_Activo");

            builder.HasIndex(u => u.TokenRecuperacion)
                .HasDatabaseName("IX_Usuarios_TokenRecuperacion")
                .HasFilter("[TokenRecuperacion] IS NOT NULL");

            builder.HasIndex(u => u.CuentaBloqueada)
                .HasDatabaseName("IX_Usuarios_CuentaBloqueada")
                .HasFilter("[CuentaBloqueada] = 1");

            // Relaciones
            builder.HasOne(u => u.Empresa)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(u => u.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar tabla con restricciones CHECK
            builder.ToTable("Usuarios", t =>
            {
                t.HasCheckConstraint("CK_Usuarios_IntentosLogin",
                    "[IntentosLoginFallidos] >= 0 AND [IntentosLoginFallidos] <= 10");

                t.HasCheckConstraint("CK_Usuarios_Idioma",
                    "[Idioma] IN ('es-CL', 'es-ES', 'en-US')");
            });
        }
    }
}
