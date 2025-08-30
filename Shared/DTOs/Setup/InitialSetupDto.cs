using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Setup
{
    /// <summary>
    /// DTO para la transferencia de datos de configuración inicial del sistema ERP
    /// Se utiliza para comunicación entre el cliente y el servidor
    /// </summary>
    public class InitialSetupDto
    {
        #region Datos de la Empresa

        public string RutEmpresa { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string? NombreComercial { get; set; }
        public string? Giro { get; set; }

        #endregion

        #region Dirección Casa Matriz

        public string? DireccionCasaMatriz { get; set; }
        public string? ComunaCasaMatriz { get; set; }
        public string? CiudadCasaMatriz { get; set; }
        public string? RegionCasaMatriz { get; set; }

        #endregion

        #region Información de Contacto

        public string? Telefono { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? SitioWeb { get; set; }

        #endregion

        #region Información SII

        public string? CodigoSII { get; set; }
        public string? ResolucionSII { get; set; }
        public DateTime? FechaResolucionSII { get; set; }

        #endregion

        #region Configuración del Sistema

        public int MonedaPrincipalId { get; set; }
        public int MetodoValorizacionId { get; set; }
        public bool PermiteInventarioNegativo { get; set; }
        public bool ManejaLotes { get; set; }
        public bool ManejaSeries { get; set; }
        public bool ManejaUbicaciones { get; set; }

        #endregion

        #region Configuraciones de Seguridad

        public bool RequiereAutenticacionDosFactor { get; set; }
        public int TiempoExpiracionSesion { get; set; }
        public int MaximoIntentosLogin { get; set; }
        public int TiempoBloqueoTemporalMinutos { get; set; }

        #endregion

        #region Usuario Administrador

        public string AdminUsuario { get; set; } = string.Empty;
        public string AdminEmail { get; set; } = string.Empty;
        public string AdminNombres { get; set; } = string.Empty;
        public string AdminApellidos { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;
        public string? AdminRut { get; set; }

        #endregion

        #region Configuraciones Regionales

        public string Idioma { get; set; } = "es-CL";
        public string ZonaHoraria { get; set; } = "America/Santiago";
        public string FormatoFecha { get; set; } = "dd/MM/yyyy";
        public string SeparadorDecimal { get; set; } = ",";

        #endregion
    }

    /// <summary>
    /// DTO de respuesta para la configuración inicial
    /// </summary>
    public class InitialSetupResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int EmpresaId { get; set; }
        public int UsuarioAdminId { get; set; }
        public List<string> Errors { get; set; } = new();

        /// <summary>
        /// Token JWT para autenticación inmediata después de la configuración
        /// </summary>
        public string? AuthToken { get; set; }

        /// <summary>
        /// Fecha de expiración del token
        /// </summary>
        public DateTime? TokenExpiration { get; set; }
    }

    /// <summary>
    /// DTO para validar si el sistema ya está configurado
    /// </summary>
    public class SystemStatusDto
    {
        public bool IsConfigured { get; set; }
        public bool HasAdmin { get; set; }
        public DateTime? ConfigurationDate { get; set; }
        public string? CompanyName { get; set; }
        public int TotalUsers { get; set; }
        public int TotalBranches { get; set; }
        public int TotalWarehouses { get; set; }
    }
}
