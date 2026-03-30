using QuantityMeasurementApp.Entity;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuantityMeasurementApp.Repository
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private static QuantityMeasurementCacheRepository? instance = null;
        private readonly List<QuantityMeasurementEntity> cache;

        private QuantityMeasurementCacheRepository()
        {
            cache = new List<QuantityMeasurementEntity>();
        }

        public static QuantityMeasurementCacheRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuantityMeasurementCacheRepository();
                }
                return instance;
            }
        }

        public void SaveMeasurement(QuantityMeasurementEntity entity)
        {
            cache.Add(entity);
            // SaveToDisk could be implemented here
        }

        public IEnumerable<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return cache;
        }

        public IEnumerable<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            return cache.FindAll(e => e.OperationType.Equals(operationType, System.StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<QuantityMeasurementEntity> GetMeasurementsByType(string measurementType)
        {
            return cache.FindAll(e => e.MeasurementType.Equals(measurementType, System.StringComparison.OrdinalIgnoreCase));
        }

        public int GetTotalCount()
        {
            return cache.Count;
        }

        public void DeleteAll()
        {
            cache.Clear();
        }
    }
}
