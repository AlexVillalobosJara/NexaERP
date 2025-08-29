using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Security
{
    /// <summary>
    /// Entidad SesionUsuario - Control de sesiones activas
    /// Gestión de tokens JWT y control de concurrencia
    /// </summary>
    public class SesionUsuario : BaseEntity
    {
        [Required]
        public int UsuarioId { get; set; }

        // Datos de la Sesión
        [Required]
        [MaxLength(255)]
        public string TokenSesion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaInicioSesion { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime FechaExpiracion { get; set; }

        [Required]
        public DateTime FechaUltimaActividad { get; set; } = DateTime.UtcNow;

        // Información del Cliente
        [Required]
        [MaxLength(45)]
        public string DireccionIP { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? UserAgent { get; set; }

        [MaxLength(100)]
        public string? Dispositivo { get; set; }

        [MaxLength(50)]
        public string? SistemaOperativo { get; set; }

        [MaxLength(50)]
        public string? Navegador { get; set; }

        // Estado de la Sesión
        [Required]
        public bool SesionActiva { get; set; } = true;

        public DateTime? FechaCierre { get; set; }

        [MaxLength(50)]
        public string? RazonCierre { get; set; }

        // Propiedades Calculadas
        public bool EstaExpirada => DateTime.UtcNow > FechaExpiracion;

        public bool RequiereRenovacion =>
            SesionActiva &&
            DateTime.UtcNow > FechaUltimaActividad.AddMinutes(30);

        public TimeSpan TiempoSesion =>
            (FechaCierre ?? DateTime.UtcNow) - FechaInicioSesion;

        // Propiedades de Navegación
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
