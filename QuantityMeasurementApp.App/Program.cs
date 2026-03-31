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

            // Setup Dependencies (Poor Man's Dependency Injection)
            IQuantityMeasurementRepository repository;

            // Simple DB setup for console app using in-memory provider
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("QuantityMeasurementDB_Console")
                .Options;

            var dbContext = new AppDbContext(options);
            repository = new EfCoreQuantityMeasurementRepository(dbContext);
            Console.WriteLine("--- Initializing system with EF Core In-Memory Repository ---");

            IQuantityMeasurementService service = new QuantityMeasurementService(repository);
            QuantityMeasurementController controller = new QuantityMeasurementController(service);

            // Initialize Menu and Hand Over Control
            IMenu menu = new Menu(controller);
            menu.Run();
        }
    }
}
