using Microsoft.EntityFrameworkCore;
using NexaERP.Infrastructure.Data.Context;

namespace NexaERP.Web.Middleware
{
    public class SetupRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public SetupRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, NexaErpDbContext dbContext)
        {
            // Permitir acceso a archivos estáticos y setup
            if (context.Request.Path.StartsWithSegments("/css") ||
                context.Request.Path.StartsWithSegments("/js") ||
                context.Request.Path.StartsWithSegments("/_framework") ||
                context.Request.Path.StartsWithSegments("/_content") ||
                context.Request.Path.StartsWithSegments("/setup"))
            {
                await _next(context);
                return;
            }

            // Verificar si el sistema está configurado
            try
            {
                var hasEmpresas = await dbContext.Empresas.AnyAsync();
                if (!hasEmpresas && !context.Request.Path.StartsWithSegments("/setup"))
                {
                    context.Response.Redirect("/setup");
                    return;
                }
            }
            catch
            {
                // En caso de error de base de datos, continuar normalmente
            }

            await _next(context);
        }
    }
}