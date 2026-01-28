using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Universidad.Data;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión desde variable de entorno o appsettings
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    Console.WriteLine($"Usando cadena de conexión de appsettings.json");
    
    // Si todavía está vacía, usar una cadena por defecto para desarrollo
    if (string.IsNullOrWhiteSpace(connectionString))
    {
        connectionString = "Host=localhost;Port=5432;Database=UniversidadDB;Username=postgres;Password=postgres;";
        Console.WriteLine($"Usando cadena de conexión por defecto para desarrollo");
    }
}

Console.WriteLine($"Cadena de conexión: {MaskPassword(connectionString)}");

// ✅ Configurar DbContext con PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Universidad API - Talleres", 
        Version = "v1",
        Description = "API para gestión de talleres universitarios",
        Contact = new OpenApiContact
        {
            Name = "Universidad",
            Email = "universidad@example.com"
        }
    });
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Intentar aplicar migraciones, pero no fallar si no hay conexión
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("Intentando aplicar migraciones...");
        db.Database.Migrate();
        Console.WriteLine("Migraciones aplicadas correctamente");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"⚠️  No se pudieron aplicar migraciones: {ex.Message}");
    Console.WriteLine("⚠️  La aplicación continuará sin conexión a base de datos para desarrollo.");
}

// Configurar el pipeline HTTP
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Talleres API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "API Talleres - Swagger UI";
});

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Puerto por defecto
var port = Environment.GetEnvironmentVariable("PORT") ?? "7272";
app.Urls.Add($"http://0.0.0.0:{port}");
app.Urls.Add($"https://0.0.0.0:7282");

Console.WriteLine($"\n✅ API de Talleres iniciada");
Console.WriteLine($"🌐 Swagger UI: http://localhost:{port}/swagger");
Console.WriteLine($"🔗 API Base: http://localhost:{port}/api/Talleres");
Console.WriteLine($"📚 Documentación: http://localhost:{port}/swagger/v1/swagger.json");
Console.WriteLine($"\n📋 Endpoints disponibles:");
Console.WriteLine($"   GET    /api/Talleres");
Console.WriteLine($"   POST   /api/Talleres");
Console.WriteLine($"   GET    /api/Talleres/{{id}}");
Console.WriteLine($"\n🚀 Listo para recibir peticiones...");

app.Run();

// Función para enmascarar la contraseña en los logs
static string MaskPassword(string connectionString)
{
    if (string.IsNullOrEmpty(connectionString)) return "[No configurada]";
    
    try
    {
        var parts = connectionString.Split(';');
        var maskedParts = parts.Select(p =>
        {
            if (p.TrimStart().StartsWith("Password=", StringComparison.OrdinalIgnoreCase) ||
                p.TrimStart().StartsWith("Pwd=", StringComparison.OrdinalIgnoreCase))
            {
                return "Password=***";
            }
            return p;
        });
        return string.Join(";", maskedParts);
    }
    catch
    {
        return "[Error parsing connection string]";
    }
}
