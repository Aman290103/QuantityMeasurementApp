using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;
using QuantityMeasurementApp.Controllers;
using System.Linq;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Quantity Measurement App Demo (N-Tier Architecture) ---\n");

            // Setup Dependencies
            IQuantityMeasurementRepository repository = QuantityMeasurementCacheRepository.Instance;
            IQuantityMeasurementService service = new QuantityMeasurementServiceImpl(repository);
            QuantityMeasurementController controller = new QuantityMeasurementController(service);

            // --- Length Operations ---
            Console.WriteLine("--- Length Demonstrations ---");
            controller.PerformComparison(
                new QuantityDTO(1.0, "Feet", "Length"), 
                new QuantityDTO(12.0, "Inch", "Length"));

            controller.PerformConversion(
                new QuantityDTO(1.0, "Feet", "Length"), "Inch");

            controller.PerformAddition(
                 new QuantityDTO(1.0, "Feet", "Length"),
                 new QuantityDTO(2.0, "Inch", "Length"), "Inch");

            // --- Volume Operations ---
            Console.WriteLine("\n--- Volume Demonstrations ---");
            controller.PerformComparison(
                new QuantityDTO(1.0, "Gallon", "Volume"), 
                new QuantityDTO(3.78541, "Litre", "Volume"));

            controller.PerformAddition(
                 new QuantityDTO(1.0, "Gallon", "Volume"),
                 new QuantityDTO(1.0, "Litre", "Volume"), "Litre");

            // --- Temperature Operations ---
            Console.WriteLine("\n--- Temperature Demonstrations ---");
            controller.PerformComparison(
                new QuantityDTO(100.0, "Celsius", "Temperature"), 
                new QuantityDTO(212.0, "Fahrenheit", "Temperature"));

            controller.PerformConversion(
                new QuantityDTO(100.0, "Celsius", "Temperature"), "Kelvin");

            Console.WriteLine("Attempting unsupported addition (100 Celsius + 50 Celsius):");
            controller.PerformAddition(
                new QuantityDTO(100.0, "Celsius", "Temperature"),
                new QuantityDTO(50.0, "Celsius", "Temperature"), "Celsius");

            var measurements = repository.GetAllMeasurements();
            Console.WriteLine($"\nStored entity count in Cache: {measurements.Count()}");
            foreach(var item in measurements)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }
    }
}