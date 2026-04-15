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

// Handle Render's postgres:// URL format if necessary
if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgres"))
{
    var databaseUri = new Uri(connectionString);
    var userInfo = databaseUri.UserInfo.Split(':');
    var username = Uri.UnescapeDataString(userInfo[0]);
    var password = Uri.UnescapeDataString(userInfo[1]);
    var host = databaseUri.Host;
    var port = databaseUri.Port == -1 ? 5432 : databaseUri.Port; // Default to 5432 if port is missing
    var database = databaseUri.AbsolutePath.TrimStart('/');
    
    connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        options.UseNpgsql(connectionString, b => b.MigrationsAssembly("QuantityMeasurementApp.Repository"));
    }
    else
    {
        options.UseInMemoryDatabase("QuantityMeasurementDB");
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

