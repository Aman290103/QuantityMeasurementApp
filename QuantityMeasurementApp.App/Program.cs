using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;
using QuantityMeasurementApp.Controllers;

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

            if (useDatabase)
            {
                string connectionString = config.GetConnectionString("SqlServer") ?? "";
                Console.WriteLine("--- Initializing system with Database Repository ---");
                repository = new QuantityMeasurementDatabaseRepository(connectionString);
            }
            else
            {
                Console.WriteLine("--- Initializing system with Local Cache Repository ---");
                repository = QuantityMeasurementCacheRepository.Instance;
            }

            IQuantityMeasurementService service = new QuantityMeasurementServiceImpl(repository);
            QuantityMeasurementController controller = new QuantityMeasurementController(service);

            // Initialize Menu and Hand Over Control
            IMenu menu = new Menu(controller);
            menu.Run();
        }
    }
}