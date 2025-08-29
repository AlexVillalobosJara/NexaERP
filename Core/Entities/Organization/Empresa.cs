using Core.Entities.Security;
using NexaERP.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Organization
{
    /// <summary>
    /// Entidad Empresa - Entidad raíz multi-tenant
    /// Base del sistema multi-tenant según especificación técnica
    /// </summary>
    public class Empresa : AuditableEntity
    {
        [Required]
        [MaxLength(12)]
        public string RutEmpresa { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string RazonSocial { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? NombreComercial { get; set; }

        [MaxLength(200)]
        public string? Giro { get; set; }

        // Dirección Casa Matriz
        [MaxLength(300)]
        public string? DireccionCasaMatriz { get; set; }

        [MaxLength(100)]
        public string? ComunaCasaMatriz { get; set; }

        [MaxLength(100)]
        public string? CiudadCasaMatriz { get; set; }

        [MaxLength(100)]
        public string? RegionCasaMatriz { get; set; }

        // Contacto
        [MaxLength(15)]
        public string? Telefono { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(150)]
        public string? SitioWeb { get; set; }

        // Control SII Chile
        [MaxLength(20)]
        public string? CodigoSII { get; set; }

        [MaxLength(100)]
        public string? ResolucionSII { get; set; }

        public DateTime? FechaResolucionSII { get; set; }

        // Configuración del Sistema
        [Required]
        public int MonedaPrincipalId { get; set; } = 1; // FK a TiposMoneda

        [Required]
        [MaxLength(20)]
        public string FormatoFecha { get; set; } = "dd/MM/yyyy";

        [Required]
        [MaxLength(1)]
        public string SeparadorDecimal { get; set; } = ",";

        // Configuraciones de Negocio
        public bool PermiteInventarioNegativo { get; set; } = false;

        [Required]
        public int MetodoValorizacionId { get; set; } = 1; // FK a MetodosValorizacion

        public bool ManejaLotes { get; set; } = true;
        public bool ManejaSeries { get; set; } = false;
        public bool ManejaUbicaciones { get; set; } = true;

        // Configuraciones de Seguridad
        public bool RequiereAutenticacionDosFactor { get; set; } = false;

        [Required]
        public int TiempoExpiracionSesion { get; set; } = 480; // Minutos (8 horas)

        [Required]
        public int MaximoIntentosLogin { get; set; } = 5;

        [Required]
        public int TiempoBloqueoTemporalMinutos { get; set; } = 30;

        // Propiedades de Navegación
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public virtual ICollection<Rol> Roles { get; set; } = new List<Rol>();
        public virtual ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();
    }
}
