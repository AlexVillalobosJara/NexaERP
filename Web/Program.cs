using NexaERP.Infrastructure.Extensions;
using NexaERP.Application.Extensions;
using NexaERP.Security.Extensions;
using Radzen;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/nexaerp-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Servicios de Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Servicios de Radzen
builder.Services.AddRadzenComponents();

// Servicios de la aplicaci�n (ORDEN IMPORTANTE)
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices(builder.Configuration);

var app = builder.Build();

// Configuraci�n del pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ORDEN IMPORTANTE: Authentication antes que Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

try
{
    Log.Information("Iniciando NexaERP...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicaci�n fall� al iniciar");
}
finally
{
    Log.CloseAndFlush();
}
