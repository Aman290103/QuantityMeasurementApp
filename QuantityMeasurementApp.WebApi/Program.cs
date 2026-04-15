using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;
using QuantityMeasurementApp.WebApi.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quantity Measurement API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var isDevelopment = builder.Environment.IsDevelopment();

// On Render, DATABASE_URL is set as an env var in postgres:// URI format
var renderDatabaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(renderDatabaseUrl))
{
    connectionString = renderDatabaseUrl;
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (string.IsNullOrEmpty(connectionString))
    {
        options.UseInMemoryDatabase("QuantityMeasurementDB");
    }
    else if (!isDevelopment && connectionString.StartsWith("postgres"))
    {
        // Strip query parameters (?sslmode=...) and trailing slashes
        var urlWithoutQuery = connectionString.Split('?')[0];
        var cleanUrl = urlWithoutQuery.Replace("postgresql://", "").Replace("postgres://", "").Trim();
        
        var atIndex = cleanUrl.LastIndexOf('@');
        var userInfo = cleanUrl.Substring(0, atIndex);
        var hostAndDb = cleanUrl.Substring(atIndex + 1).TrimEnd('/');

        var firstSlash = hostAndDb.IndexOf('/');
        var hostAndPort = firstSlash > -1 ? hostAndDb.Substring(0, firstSlash) : hostAndDb;
        var db = firstSlash > -1 ? hostAndDb.Substring(firstSlash + 1) : "";

        var user = userInfo.Split(':')[0];
        var pass = userInfo.Contains(':') ? userInfo.Split(':')[1] : "";

        var host = hostAndPort.Contains(':') ? hostAndPort.Split(':')[0] : hostAndPort;
        var port = hostAndPort.Contains(':') ? hostAndPort.Split(':')[1] : "5432";

        // Enforce full internal domain for Render if missing
        if (!host.Contains("."))
        {
            host = $"{host}.oregon-postgres.render.com";
        }

        // Fallback: In Postgres, username is often the database name if unspecified
        if (string.IsNullOrEmpty(db))
        {
            db = user;
        }

        var pgConnStr = $"Host={host};Port={port};Database={db};Username={user};Password={pass};SSL Mode=Require;Trust Server Certificate=true;Pooling=true;";
        
        Console.WriteLine($"[DEBUG] REFINED CONFIG -> Host: {host}, Database: {db}, Username: {user}");

        options.UseNpgsql(pgConnStr, b => 
        {
            b.MigrationsAssembly("QuantityMeasurementApp.Repository");
            b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
    }
    else
    {
        // Development (local): use SQL Server (SSMS)
        options.UseSqlServer(connectionString, b => b.MigrationsAssembly("QuantityMeasurementApp.Repository"));
    }
});

// Configure JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found");
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

builder.Services.AddScoped<IQuantityMeasurementRepository, EfCoreQuantityMeasurementRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();
builder.Services.AddScoped<IUserRepository, EfCoreUserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Global Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Redirect root to Swagger
app.MapGet("/", (context) => 
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

// Migrate DataBase
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        Console.WriteLine("\n--- Applying Database Migrations (if any) ---");
        dbContext.Database.Migrate();
        Console.WriteLine("--- Database Migrations Applied Successfully ---\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n--- Error Applying Migrations: {ex.Message} ---\n");
    }
}

app.Run();

