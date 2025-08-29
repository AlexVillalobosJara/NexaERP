using Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexaERP.Core.Interfaces.Repositories.Base;
using NexaERP.Infrastructure.Data.Context;
using NexaERP.Infrastructure.Repositories.Base;

namespace NexaERP.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configuración de Entity Framework
            services.AddDbContext<NexaErpDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });

                // Configuraciones adicionales para desarrollo
                options.EnableSensitiveDataLogging(false);
                options.EnableServiceProviderCaching();
            });

            // Registro de repositorios genéricos
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // TODO: Agregar repositorios específicos cuando se implementen
            // services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            // services.AddScoped<IEmpresaRepository, EmpresaRepository>();

            return services;
        }

        /// <summary>
        /// Inicializar base de datos con datos semilla
        /// </summary>
        public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NexaErpDbContext>();

            try
            {
                // Aplicar migraciones pendientes
                await context.Database.MigrateAsync();

                // Aplicar datos semilla de seguridad
                await SecuritySeedData.SeedAsync(context);
            }
            catch (Exception ex)
            {
                // TODO: Agregar logging
                throw new Exception("Error al inicializar la base de datos", ex);
            }
        }
    }
}