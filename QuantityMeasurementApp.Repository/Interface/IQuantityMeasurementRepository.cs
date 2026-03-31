using System.Collections.Generic;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository
{
    public interface IQuantityMeasurementRepository
    {
        void SaveMeasurement(QuantityMeasurementEntity entity);
        IEnumerable<QuantityMeasurementEntity> GetAllMeasurements();
        
        IEnumerable<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType);
        IEnumerable<QuantityMeasurementEntity> GetMeasurementsByType(string measurementType);
        int GetTotalCount();
        void DeleteAll();

        IEnumerable<QuantityMeasurementEntity> GetErrorMeasurements();
        int GetOperationCount(string op);
    }
}
