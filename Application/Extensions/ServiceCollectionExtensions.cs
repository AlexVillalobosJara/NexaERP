using Application.Services.Security;
using Core.Interfaces.Services.Security;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace NexaERP.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Obtener el assembly actual
            var assembly = typeof(ServiceCollectionExtensions).Assembly;

            // Registrar AutoMapper
            services.AddAutoMapper(assembly);

            // Registrar FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            // Registrar servicios de aplicación
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // TODO: Agregar otros servicios cuando se implementen
            // services.AddScoped<IUsuarioService, UsuarioService>();
            // services.AddScoped<IEmpresaService, EmpresaService>();

            return services;
        }
    }
}