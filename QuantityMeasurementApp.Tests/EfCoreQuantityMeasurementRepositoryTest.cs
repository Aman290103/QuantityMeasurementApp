using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuantityMeasurementApp.Entity;
using QuantityMeasurementApp.Repository;
using System.Linq;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class EfCoreQuantityMeasurementRepositoryTest
    {
        private AppDbContext _context;
        private EfCoreQuantityMeasurementRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestQuantityMeasurementDB_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new EfCoreQuantityMeasurementRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void TestSaveMeasurement_PersistsToInMemoryDatabase()
        {
            var entity = new QuantityMeasurementEntity("CONVERT", 1, "Feet", null, null, 12, "Inch", false, null, "Length");
            _repository.SaveMeasurement(entity);

            var count = _repository.GetTotalCount();
            Assert.AreEqual(1, count);
            
            var saved = _repository.GetAllMeasurements().First();
            Assert.AreEqual("Feet", saved.Operand1Unit);
            Assert.AreEqual(12, saved.ResultValue);
        }

        [Test]
        public void TestGetAllMeasurements_ReturnsAllSaved()
        {
            _repository.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Feet", 2, "Inch", 14, "Inch", false, null, "Length"));
            _repository.SaveMeasurement(new QuantityMeasurementEntity("CONVERT", 1, "Feet", null, null, 12, "Inch", false, null, "Length"));

            var all = _repository.GetAllMeasurements();
            Assert.AreEqual(2, all.Count());
        }

        [Test]
        public void TestGetMeasurementsByOperation_FiltersCorrectly()
        {
            _repository.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Feet", null, null, null, null, false, null, "Length"));
            _repository.SaveMeasurement(new QuantityMeasurementEntity("COMPARE", 1, "Feet", null, null, null, null, false, null, "Length"));
            _repository.SaveMeasurement(new QuantityMeasurementEntity("COMPARE", 2, "Feet", null, null, null, null, false, null, "Length"));

            var filtered = _repository.GetMeasurementsByOperation("COMPARE");
            Assert.AreEqual(2, filtered.Count());
        }

        [Test]
        public void TestGetMeasurementsByType_FiltersCorrectly()
        {
            _repository.SaveMeasurement(new QuantityMeasurementEntity("CONVERT", 1, "Feet", null, null, null, null, false, null, "Length"));
            _repository.SaveMeasurement(new QuantityMeasurementEntity("CONVERT", 100, "Celsius", null, null, null, null, false, null, "Temperature"));
            _repository.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Gallon", null, null, null, null, false, null, "Volume"));

            var tempResults = _repository.GetMeasurementsByType("Temperature");
            Assert.AreEqual(1, tempResults.Count());
            Assert.AreEqual("Celsius", tempResults.First().Operand1Unit);
        }

        [Test]
        public void TestGetOperationCount_ReturnsCorrectNumber()
        {
            _repository.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Feet", null, null, null, null, false, null, "Length"));
            _repository.SaveMeasurement(new QuantityMeasurementEntity("ADD", 2, "Feet", null, null, null, null, false, null, "Length"));
            
            Assert.AreEqual(2, _repository.GetOperationCount("ADD"));
            Assert.AreEqual(0, _repository.GetOperationCount("CONVERT"));
        }

        [Test]
        public void TestGetErrorMeasurements_ReturnsOnlyErrored()
        {
            _repository.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Feet", null, null, null, null, false, null, "Length"));
            _repository.SaveMeasurement(new QuantityMeasurementEntity("CONVERT", 1, "Feet", null, null, null, null, true, "Error occurred", "Length"));

            var errors = _repository.GetErrorMeasurements();
            Assert.AreEqual(1, errors.Count());
            Assert.IsTrue(errors.First().HasError);
        }

        [Test]
        public void TestDeleteAll_ClearsTable()
        {
            _repository.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Feet", null, null, null, null, false, null, "Length"));
            _repository.DeleteAll();
            Assert.AreEqual(0, _repository.GetTotalCount());
        }
    }
}
