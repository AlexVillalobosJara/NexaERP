using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NexaERP.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context
{
    /// <summary>
    /// Factory para crear DbContext en tiempo de diseño (migraciones)
    /// Implementa IDesignTimeDbContextFactory para resolver dependencias
    /// </summary>
    public class NexaErpDbContextFactory : IDesignTimeDbContextFactory<NexaErpDbContext>
    {
        public NexaErpDbContext CreateDbContext(string[] args)
        {
            // Configurar DbContextOptions para tiempo de diseño
            var optionsBuilder = new DbContextOptionsBuilder<NexaErpDbContext>();

            // Connection string para desarrollo
            var connectionString = "Server=DESKTOP-MPMD4K2\\SQLEXPRESS; Database=NexaERP;user id=NexaERPUser;pwd=NexaERPUser; TrustServerCertificate=True";

            // Configurar SQL Server
            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });

            // Configuraciones adicionales para desarrollo
            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.EnableServiceProviderCaching();

            return new NexaErpDbContext(optionsBuilder.Options);
        }
    }
}
