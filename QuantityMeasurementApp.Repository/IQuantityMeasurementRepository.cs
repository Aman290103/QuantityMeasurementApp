using QuantityMeasurementApp.Models;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Repository
{
    public interface IQuantityMeasurementRepository
    {
        void SaveMeasurement(QuantityMeasurementEntity entity);
        IEnumerable<QuantityMeasurementEntity> GetAllMeasurements();
    }
}
