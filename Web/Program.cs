using NexaERP.Application.Extensions;
using NexaERP.Infrastructure.Extensions;
using NexaERP.Security.Extensions;
using Web.Components;
using NexaERP.Web.Middleware;
using Radzen;
using Serilog;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/nexaerp-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Servicios
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddAutoMapper(typeof(Program));

// Servicios de la aplicación
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices(builder.Configuration);

var app = builder.Build();

// Pipeline de configuración
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ⭐ AGREGAR MIDDLEWARE DE SETUP
app.UseMiddleware<SetupRedirectMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

try
{
    Log.Information("Iniciando NexaERP...");

    // Inicializar base de datos con datos semilla básicos
    await app.Services.InitializeDatabaseAsync();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}