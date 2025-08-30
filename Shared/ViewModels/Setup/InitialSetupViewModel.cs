using System.ComponentModel.DataAnnotations;
using Shared.DTOs.Setup;

namespace Shared.ViewModels.Setup
{
    /// <summary>
    /// ViewModel para la configuración inicial del sistema ERP
    /// Incluye todos los datos necesarios para crear la empresa base y el usuario administrador
    /// </summary>
    public class InitialSetupViewModel
    {
        #region Datos de la Empresa

        [Required(ErrorMessage = "El RUT de la empresa es obligatorio")]
        [StringLength(12, ErrorMessage = "El RUT no puede exceder 12 caracteres")]
        [RegularExpression(@"^\d{1,2}\.\d{3}\.\d{3}-[\dkK]$", ErrorMessage = "Formato de RUT inválido (ej: 12.345.678-9)")]
        public string RutEmpresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "La razón social es obligatoria")]
        [StringLength(200, ErrorMessage = "La razón social no puede exceder 200 caracteres")]
        public string RazonSocial { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "El nombre comercial no puede exceder 150 caracteres")]
        public string? NombreComercial { get; set; }

        [StringLength(200, ErrorMessage = "El giro no puede exceder 200 caracteres")]
        public string? Giro { get; set; }

        #endregion

        #region Dirección Casa Matriz

        [StringLength(300, ErrorMessage = "La dirección no puede exceder 300 caracteres")]
        public string? DireccionCasaMatriz { get; set; }

        [StringLength(100, ErrorMessage = "La comuna no puede exceder 100 caracteres")]
        public string? ComunaCasaMatriz { get; set; }

        [StringLength(100, ErrorMessage = "La ciudad no puede exceder 100 caracteres")]
        public string? CiudadCasaMatriz { get; set; }

        [StringLength(100, ErrorMessage = "La región no puede exceder 100 caracteres")]
        public string? RegionCasaMatriz { get; set; }

        #endregion

        #region Información de Contacto

        [StringLength(15, ErrorMessage = "El teléfono no puede exceder 15 caracteres")]
        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "El sitio web no puede exceder 150 caracteres")]
        [Url(ErrorMessage = "Formato de URL inválido")]
        public string? SitioWeb { get; set; }

        #endregion

        #region Información SII

        [StringLength(20, ErrorMessage = "El código SII no puede exceder 20 caracteres")]
        public string? CodigoSII { get; set; }

        [StringLength(100, ErrorMessage = "La resolución SII no puede exceder 100 caracteres")]
        public string? ResolucionSII { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaResolucionSII { get; set; }

        #endregion

        #region Configuración del Sistema

        [Required(ErrorMessage = "Debe seleccionar una moneda principal")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una moneda principal válida")]
        public int MonedaPrincipalId { get; set; } = 1; // CLP por defecto

        [Required(ErrorMessage = "Debe seleccionar un método de valorización")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un método de valorización válido")]
        public int MetodoValorizacionId { get; set; } = 1; // Promedio Ponderado por defecto

        public bool PermiteInventarioNegativo { get; set; } = false;

        public bool ManejaLotes { get; set; } = true;

        public bool ManejaSeries { get; set; } = false;

        public bool ManejaUbicaciones { get; set; } = true;

        #endregion

        #region Configuraciones de Seguridad

        public bool RequiereAutenticacionDosFactor { get; set; } = false;

        [Range(30, 1440, ErrorMessage = "El tiempo de expiración debe estar entre 30 y 1440 minutos")]
        public int TiempoExpiracionSesion { get; set; } = 480; // 8 horas por defecto

        [Range(3, 10, ErrorMessage = "Los intentos máximos deben estar entre 3 y 10")]
        public int MaximoIntentosLogin { get; set; } = 5;

        [Range(5, 180, ErrorMessage = "El tiempo de bloqueo debe estar entre 5 y 180 minutos")]
        public int TiempoBloqueoTemporalMinutos { get; set; } = 30;

        #endregion

        #region Usuario Administrador

        [Required(ErrorMessage = "El nombre de usuario administrador es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9_\.]+$", ErrorMessage = "Solo se permiten letras, números, punto y guión bajo")]
        public string AdminUsuario { get; set; } = "admin";

        [Required(ErrorMessage = "El email del administrador es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        public string AdminEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Los nombres deben tener entre 2 y 100 caracteres")]
        public string AdminNombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Los apellidos deben tener entre 2 y 100 caracteres")]
        public string AdminApellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "La contraseña debe contener al menos: 1 minúscula, 1 mayúscula, 1 número y 1 carácter especial")]
        public string AdminPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [Compare(nameof(AdminPassword), ErrorMessage = "Las contraseñas no coinciden")]
        public string AdminPasswordConfirm { get; set; } = string.Empty;

        [StringLength(12, ErrorMessage = "El RUT no puede exceder 12 caracteres")]
        [RegularExpression(@"^\d{1,2}\.\d{3}\.\d{3}-[\dkK]$", ErrorMessage = "Formato de RUT inválido (ej: 12.345.678-9)")]
        public string? AdminRut { get; set; }

        #endregion

        #region Configuraciones Regionales

        public string Idioma { get; set; } = "es-CL";

        public string ZonaHoraria { get; set; } = "America/Santiago";

        public string FormatoFecha { get; set; } = "dd/MM/yyyy";

        public string SeparadorDecimal { get; set; } = ",";

        #endregion

        #region Métodos de Validación

        /// <summary>
        /// Valida que todos los campos obligatorios estén completos
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(RutEmpresa) &&
                   !string.IsNullOrWhiteSpace(RazonSocial) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(AdminUsuario) &&
                   !string.IsNullOrWhiteSpace(AdminEmail) &&
                   !string.IsNullOrWhiteSpace(AdminNombres) &&
                   !string.IsNullOrWhiteSpace(AdminApellidos) &&
                   !string.IsNullOrWhiteSpace(AdminPassword) &&
                   AdminPassword == AdminPasswordConfirm &&
                   MonedaPrincipalId > 0 &&
                   MetodoValorizacionId > 0;
        }

        /// <summary>
        /// Valida el formato del RUT chileno usando el algoritmo del dígito verificador
        /// </summary>
        public static bool ValidarRutChileno(string? rut)
        {
            if (string.IsNullOrWhiteSpace(rut))
                return false;

            try
            {
                // Remover puntos y guión
                var rutLimpio = rut.Replace(".", "").Replace("-", "").ToUpper();

                if (rutLimpio.Length < 2)
                    return false;

                // Separar número y dígito verificador
                var numero = rutLimpio[..^1];
                var dv = rutLimpio[^1];

                if (!int.TryParse(numero, out int rutNumero))
                    return false;

                // Calcular dígito verificador
                var suma = 0;
                var multiplicador = 2;

                for (int i = numero.Length - 1; i >= 0; i--)
                {
                    suma += int.Parse(numero[i].ToString()) * multiplicador;
                    multiplicador = multiplicador == 7 ? 2 : multiplicador + 1;
                }

                var resto = suma % 11;
                var dvCalculado = resto == 0 ? "0" : resto == 1 ? "K" : (11 - resto).ToString();

                return dv.ToString() == dvCalculado;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Formatea el RUT con puntos y guión
        /// </summary>
        public static string FormatearRut(string rut)
        {
            if (string.IsNullOrWhiteSpace(rut))
                return string.Empty;

            var rutLimpio = rut.Replace(".", "").Replace("-", "").ToUpper();

            if (rutLimpio.Length < 2)
                return rut;

            var numero = rutLimpio[..^1];
            var dv = rutLimpio[^1];

            // Formatear con puntos
            var rutFormateado = "";
            for (int i = numero.Length - 1, j = 0; i >= 0; i--, j++)
            {
                if (j > 0 && j % 3 == 0)
                    rutFormateado = "." + rutFormateado;
                rutFormateado = numero[i] + rutFormateado;
            }

            return rutFormateado + "-" + dv;
        }

        /// <summary>
        /// Genera una contraseña segura aleatoria
        /// </summary>
        public static string GenerarPasswordSegura(int longitud = 12)
        {
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "0123456789";
            const string especiales = "@$!%*?&";
            const string todos = minusculas + mayusculas + numeros + especiales;

            var random = new Random();
            var password = new char[longitud];

            // Asegurar al menos un carácter de cada tipo
            password[0] = minusculas[random.Next(minusculas.Length)];
            password[1] = mayusculas[random.Next(mayusculas.Length)];
            password[2] = numeros[random.Next(numeros.Length)];
            password[3] = especiales[random.Next(especiales.Length)];

            // Completar el resto aleatoriamente
            for (int i = 4; i < longitud; i++)
            {
                password[i] = todos[random.Next(todos.Length)];
            }

            // Mezclar el array
            for (int i = 0; i < longitud; i++)
            {
                int j = random.Next(i, longitud);
                (password[i], password[j]) = (password[j], password[i]);
            }

            return new string(password);
        }

        #endregion

        #region Métodos de Conversión

        /// <summary>
        /// Convierte el ViewModel a DTO para envío a la API
        /// </summary>
        public InitialSetupDto ToDto()
        {
            return new InitialSetupDto
            {
                // Datos de la empresa
                RutEmpresa = RutEmpresa?.Trim(),
                RazonSocial = RazonSocial?.Trim(),
                NombreComercial = NombreComercial?.Trim(),
                Giro = Giro?.Trim(),
                DireccionCasaMatriz = DireccionCasaMatriz?.Trim(),
                ComunaCasaMatriz = ComunaCasaMatriz?.Trim(),
                CiudadCasaMatriz = CiudadCasaMatriz?.Trim(),
                RegionCasaMatriz = RegionCasaMatriz?.Trim(),
                Telefono = Telefono?.Trim(),
                Email = Email?.Trim(),
                SitioWeb = SitioWeb?.Trim(),

                // Información SII
                CodigoSII = CodigoSII?.Trim(),
                ResolucionSII = ResolucionSII?.Trim(),
                FechaResolucionSII = FechaResolucionSII,

                // Configuración del sistema
                MonedaPrincipalId = MonedaPrincipalId,
                MetodoValorizacionId = MetodoValorizacionId,
                PermiteInventarioNegativo = PermiteInventarioNegativo,
                ManejaLotes = ManejaLotes,
                ManejaSeries = ManejaSeries,
                ManejaUbicaciones = ManejaUbicaciones,

                // Configuraciones de seguridad
                RequiereAutenticacionDosFactor = RequiereAutenticacionDosFactor,
                TiempoExpiracionSesion = TiempoExpiracionSesion,
                MaximoIntentosLogin = MaximoIntentosLogin,
                TiempoBloqueoTemporalMinutos = TiempoBloqueoTemporalMinutos,

                // Usuario administrador
                AdminUsuario = AdminUsuario?.Trim(),
                AdminEmail = AdminEmail?.Trim(),
                AdminNombres = AdminNombres?.Trim(),
                AdminApellidos = AdminApellidos?.Trim(),
                AdminPassword = AdminPassword, // No hacer trim a la contraseña
                AdminRut = AdminRut?.Trim(),

                // Configuraciones regionales
                Idioma = Idioma,
                ZonaHoraria = ZonaHoraria,
                FormatoFecha = FormatoFecha,
                SeparadorDecimal = SeparadorDecimal
            };
        }

        #endregion
    }
}
