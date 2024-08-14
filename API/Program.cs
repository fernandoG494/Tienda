using API.Extensions;
using AspNetCoreRateLimit;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext().CreateLogger();

//builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
builder.Services.ConfigureApiVersioning();
builder.Services.AddJwt(builder.Configuration);  

builder.Services.AddControllers(options => {
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters();

builder.Services.AddDbContext<TiendaContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
app.UseIpRateLimiting();

// seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<TiendaContext>();
        await context.Database.MigrateAsync();
        await TiendaContextSeed.SeedAsync(context, loggerFactory);
        await TiendaContextSeed.SeedRolesAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var _logger = loggerFactory.CreateLogger<TiendaContext>();
        _logger.LogError(ex, "Error during migration");
    }
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
