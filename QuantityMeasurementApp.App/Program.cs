using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;
using QuantityMeasurementApp.Controller;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            bool useDatabase = false;
            bool.TryParse(config["UseDatabase"], out useDatabase);

            // Setup Dependencies (Dynamic Provider based on config)
            var connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("QuantityMeasurementApp.Repository"));
                Console.WriteLine("--- Initializing system with SQL Server Repository ---");
            }
            else
            {
                optionsBuilder.UseInMemoryDatabase("QuantityMeasurementDB_Console");
                Console.WriteLine("--- Initializing system with EF Core In-Memory Repository (Fallback) ---");
            }

            var dbContext = new AppDbContext(optionsBuilder.Options);
            
            // Apply migrations for SQL Server provider if needed
            if (!string.IsNullOrEmpty(connectionString))
            {
                dbContext.Database.Migrate();
                Console.WriteLine("--- Database Migrations Applied Successfully ---\n");
            }

            var repository = new EfCoreQuantityMeasurementRepository(dbContext);
            IQuantityMeasurementService service = new QuantityMeasurementService(repository);
            QuantityMeasurementController controller = new QuantityMeasurementController(service);

            // Initialize Menu and Hand Over Control
            IMenu menu = new Menu(controller);
            menu.Run();
        }
    }
}
