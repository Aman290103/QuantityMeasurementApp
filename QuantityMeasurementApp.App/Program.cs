using System;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;
using QuantityMeasurementApp.Controllers;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup Dependencies (Poor Man's Dependency Injection)
            IQuantityMeasurementRepository repository = QuantityMeasurementCacheRepository.Instance;
            IQuantityMeasurementService service = new QuantityMeasurementServiceImpl(repository);
            QuantityMeasurementController controller = new QuantityMeasurementController(service);

            // Initialize Menu and Hand Over Control
            IMenu menu = new Menu(controller);
            menu.Run();
        }
    }
}