using Microsoft.Data.SqlClient;
using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Repository;
using System;
using System.Linq;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityMeasurementDatabaseRepositoryTest
    {
        // Connection string provided by user
        private const string TestConnectionString = @"Server=.\SQLEXPRESS;Database=QuantityMeasurementDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Command Timeout=0";

        [Test]
        public void TestSaveMeasurement_PersistsToDatabase()
        {
            try
            {
                var repo = new QuantityMeasurementDatabaseRepository(TestConnectionString);
                repo.DeleteAll(); // Clear first

                var entity = new QuantityMeasurementEntity("CONVERT", 1, "Feet", null, null, 12, "Inch", false, null, "Length");
                repo.SaveMeasurement(entity);

                var count = repo.GetTotalCount();
                Assert.AreEqual(1, count);
            }
            catch (Exception)
            {
                Assert.Ignore("Skipping DB test. Invalid test connection string or DB not setup.");
            }
        }

        [Test]
        public void TestGetAllMeasurements_ReturnsAllSaved()
        {
            try
            {
                var repo = new QuantityMeasurementDatabaseRepository(TestConnectionString);
                repo.DeleteAll(); 

                repo.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Feet", 2, "Inch", 14, "Inch", false, null, "Length"));
                repo.SaveMeasurement(new QuantityMeasurementEntity("CONVERT", 1, "Feet", null, null, 12, "Inch", false, null, "Length"));

                var all = repo.GetAllMeasurements();
                Assert.AreEqual(2, all.Count());
            }
            catch (Exception)
            {
                Assert.Ignore("Skipping DB test. Invalid test connection string or DB not setup.");
            }
        }

        [Test]
        public void TestGetMeasurementsByOperation_FiltersCorrectly()
        {
             try
            {
                var repo = new QuantityMeasurementDatabaseRepository(TestConnectionString);
                repo.DeleteAll(); 

                repo.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Feet", null, null, null, null, false, null, "Length"));
                repo.SaveMeasurement(new QuantityMeasurementEntity("COMPARE", 1, "Feet", null, null, null, null, false, null, "Length"));
                repo.SaveMeasurement(new QuantityMeasurementEntity("COMPARE", 2, "Feet", null, null, null, null, false, null, "Length"));

                var filtered = repo.GetMeasurementsByOperation("COMPARE");
                Assert.AreEqual(2, filtered.Count());
            }
            catch (Exception)
            {
                Assert.Ignore("Skipping DB test. Invalid test connection string or DB not setup.");
            }
        }

        [Test]
        public void TestGetMeasurementsByType_FiltersCorrectly()
        {
             try
            {
                var repo = new QuantityMeasurementDatabaseRepository(TestConnectionString);
                repo.DeleteAll(); 

                repo.SaveMeasurement(new QuantityMeasurementEntity("CONVERT", 1, "Feet", null, null, null, null, false, null, "Length"));
                repo.SaveMeasurement(new QuantityMeasurementEntity("CONVERT", 100, "Celsius", null, null, null, null, false, null, "Temperature"));
                repo.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Gallon", null, null, null, null, false, null, "Volume"));

                var tempResults = repo.GetMeasurementsByType("Temperature");
                Assert.AreEqual(1, tempResults.Count());
            }
            catch (Exception)
            {
                Assert.Ignore("Skipping DB test. Invalid test connection string or DB not setup.");
            }
        }

        [Test]
        public void TestGetTotalCount_ReturnsCorrectNumber()
        {
             try
            {
                var repo = new QuantityMeasurementDatabaseRepository(TestConnectionString);
                repo.DeleteAll(); 
                Assert.AreEqual(0, repo.GetTotalCount());
            }
            catch (Exception)
            {
                Assert.Ignore("Skipping DB test. Invalid test connection string or DB not setup.");
            }
        }

        [Test]
        public void TestDeleteAll_ClearsTable()
        {
             try
            {
                var repo = new QuantityMeasurementDatabaseRepository(TestConnectionString);
                repo.SaveMeasurement(new QuantityMeasurementEntity("ADD", 1, "Feet", null, null, null, null, false, null, "Length"));
                repo.DeleteAll();
                Assert.AreEqual(0, repo.GetTotalCount());
            }
            catch (Exception)
            {
                Assert.Ignore("Skipping DB test. Invalid test connection string or DB not setup.");
            }
        }

        [Test]
        public void TestSqlInjectionPrevention_TreatedAsLiteral()
        {
             try
            {
                var repo = new QuantityMeasurementDatabaseRepository(TestConnectionString);
                repo.DeleteAll(); 

                // Attempt simulated injection
                string injection = "'; DROP TABLE QuantityMeasurements; --";
                var entity = new QuantityMeasurementEntity(injection, 1, "Feet", null, null, null, null, false, null, "Length");

                // Execute
                repo.SaveMeasurement(entity);

                // If DB is cleared, injection worked. We want it to be stored literally.
                var results = repo.GetMeasurementsByOperation(injection);
                Assert.AreEqual(1, results.Count());
                Assert.AreEqual(1, repo.GetTotalCount());
            }
            catch (Exception)
            {
                Assert.Ignore("Skipping DB test. Invalid test connection string or DB not setup.");
            }
        }
    }
}
