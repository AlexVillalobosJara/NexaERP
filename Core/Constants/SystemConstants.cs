using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    /// <summary>
    /// Constantes generales del sistema
    /// </summary>
    public static class SystemConstants
    {
        // Configuración regional Chile
        public const string DefaultCulture = "es-CL";
        public const string DefaultTimeZone = "America/Santiago";
        public const string DateFormat = "dd/MM/yyyy";
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";
        public const string DecimalSeparator = ",";
        public const string ThousandsSeparator = ".";

        // Configuración de sistema
        public const string SystemUserName = "SISTEMA";
        public const int SystemUserId = 1;
        public const string DefaultCurrency = "CLP";

        // Límites del sistema
        public const int MaxUploadFileSizeMB = 10;
        public const int DefaultPageSize = 25;
        public const int MaxPageSize = 100;
        public const int MaxSearchResults = 1000;

        // Configuración de logging
        public const string ApplicationName = "NexaERP";
        public const int LogRetentionDays = 365;
        public const int AuditRetentionDays = 2555; // 7 años para Chile

        // Mensajes del sistema
        public const string SuccessMessage = "Operación realizada exitosamente";
        public const string ErrorMessage = "Ha ocurrido un error inesperado";
        public const string ValidationErrorMessage = "Por favor corrija los errores indicados";
        public const string UnauthorizedMessage = "No tiene permisos para realizar esta operación";
        public const string NotFoundMessage = "El registro solicitado no fue encontrado";
    }
}
