using AutoMapper;
using Core.DTOs.Security;
using Core.Entities.Security;

namespace Shared.Mappings
{
    public class SecurityMappingProfile : Profile
    {
        public SecurityMappingProfile()
        {
            // Usuario mappings
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.Apellidos}"))
                .ForMember(dest => dest.EmpresaNombre, opt => opt.Ignore())
                .ForMember(dest => dest.EmpresaRut, opt => opt.Ignore());

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.Empresa, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioRoles, opt => opt.Ignore())
                .ForMember(dest => dest.Sesiones, opt => opt.Ignore())
                .ForMember(dest => dest.AuditoriasAcceso, opt => opt.Ignore());
        }
    }
}
