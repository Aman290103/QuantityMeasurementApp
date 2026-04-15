using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quantity Measurement Identity Service", Version = "v1" });
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
        options.UseInMemoryDatabase("IdentityDB");
    }
    else
    {
        // Use the connection logic we already verified works
        var host = "dpg-d7fmusnlk1mc73dhvntg-a.oregon-postgres.render.com";
        var db = "quantity_measurement_db_g33m";
        var user = "quantitymeasurementapp_user";
        var pass = "uD948vobsgiSmuoDGTSUCt7Rei4ZIKDl";
        var pgConnStr = $"Host={host};Port=5432;Database={db};Username={user};Password={pass};SSL Mode=Require;Trust Server Certificate=true;Pooling=true;";
        
        options.UseNpgsql(pgConnStr, b => b.MigrationsAssembly("QuantityMeasurementApp.Repository"));
    }
});

// Configure JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "YourSuperSecretKeyForDevelopmentOnly123!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddScoped<IUserRepository, EfCoreUserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
