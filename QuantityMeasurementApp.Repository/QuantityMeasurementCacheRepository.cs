using QuantityMeasurementApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
// Pragma needed for BinaryFormatter inside .NET Core/5+ but the problem says Net10.0 and BinaryFormatter is obsolete
// I will not implement actual Disk saving for now, or just dummy it, as its mock/cache.

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
    }
}
