using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Quantity Measurement Service", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var renderDatabaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(renderDatabaseUrl))
{
    connectionString = renderDatabaseUrl;
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (string.IsNullOrEmpty(connectionString))
    {
        options.UseInMemoryDatabase("MeasurementDB");
    }
    else
    {
        var host = "dpg-d7fmusnlk1mc73dhvntg-a.oregon-postgres.render.com";
        var db = "quantity_measurement_db_g33m";
        var user = "quantitymeasurementapp_user";
        var pass = "uD948vobsgiSmuoDGTSUCt7Rei4ZIKDl";
        var pgConnStr = $"Host={host};Port=5432;Database={db};Username={user};Password={pass};SSL Mode=Require;Trust Server Certificate=true;Pooling=true;";
        
        options.UseNpgsql(pgConnStr, b => b.MigrationsAssembly("QuantityMeasurementApp.Repository"));
    }
});

builder.Services.AddScoped<IQuantityMeasurementRepository, EfCoreQuantityMeasurementRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
