using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    /// <summary>
    /// Constantes relacionadas con la seguridad del sistema
    /// Basado en la configuración definida en la especificación técnica
    /// </summary>
    public static class SecurityConstants
    {
        // Configuración de contraseñas
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 100;
        public const int DefaultMaxLoginAttempts = 5;
        public const int DefaultLockoutTimeMinutes = 30;

        // Configuración de sesiones
        public const int DefaultSessionTimeoutMinutes = 480; // 8 horas
        public const int MaxConcurrentSessions = 3;
        public const int InactivityTimeoutMinutes = 30;

        // Configuración de tokens
        public const int PasswordResetTokenExpiryHours = 2;
        public const int EmailVerificationTokenExpiryHours = 24;

        // Configuración 2FA
        public const string TwoFactorIssuer = "NexaERP";
        public const int TwoFactorCodeLength = 6;

        // Roles del sistema (predefinidos)
        public const string AdminRole = "Administrador";
        public const string SupervisorInventarioRole = "Supervisor Inventario";
        public const string OperadorBodegaRole = "Operador Bodega";
        public const string ConsultaRole = "Consulta";

        // Claims personalizados
        public const string EmpresaIdClaim = "empresa_id";
        public const string SucursalIdClaim = "sucursal_id";
        public const string BodegaIdClaim = "bodega_id";
        public const string FullNameClaim = "full_name";
        public const string TwoFactorClaim = "2fa_enabled";

        // Políticas de autorización
        public const string RequireAdminPolicy = "RequireAdmin";
        public const string RequireSupervisorPolicy = "RequireSupervisor";
        public const string RequireOperatorPolicy = "RequireOperator";
        public const string RequireSameCompanyPolicy = "RequireSameCompany";
    }
}
