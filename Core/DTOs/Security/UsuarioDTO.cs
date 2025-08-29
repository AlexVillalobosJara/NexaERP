
namespace Core.DTOs.Security
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string? RutPersona { get; set; }
        public bool AutenticacionDobleFactorHabilitada { get; set; }
        public bool EmailVerificado { get; set; }
        public string Idioma { get; set; } = "es-CL";
        public string ZonaHoraria { get; set; } = "America/Santiago";
        public DateTime? FechaUltimoLogin { get; set; }
        public bool Activo { get; set; }

        // Propiedades calculadas
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        // Información de empresa
        public string? EmpresaNombre { get; set; }
        public string? EmpresaRut { get; set; }
    }
}
