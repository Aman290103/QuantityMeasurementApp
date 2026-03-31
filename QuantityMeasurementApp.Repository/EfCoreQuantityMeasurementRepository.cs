using System.Collections.Generic;
using System.Linq;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository
{
    public class EfCoreQuantityMeasurementRepository : IQuantityMeasurementRepository
    {
        private readonly AppDbContext _dbContext;

        public EfCoreQuantityMeasurementRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveMeasurement(QuantityMeasurementEntity entity)
        {
            _dbContext.QuantityMeasurements.Add(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return _dbContext.QuantityMeasurements.ToList();
        }

        public IEnumerable<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            return _dbContext.QuantityMeasurements.Where(q => q.OperationType == operationType).ToList();
        }

        public IEnumerable<QuantityMeasurementEntity> GetMeasurementsByType(string measurementType)
        {
            return _dbContext.QuantityMeasurements.Where(q => q.MeasurementType == measurementType).ToList();
        }

        public int GetTotalCount()
        {
            return _dbContext.QuantityMeasurements.Count();
        }

        public void DeleteAll()
        {
            var all = _dbContext.QuantityMeasurements.ToList();
            _dbContext.QuantityMeasurements.RemoveRange(all);
            _dbContext.SaveChanges();
        }

        public IEnumerable<QuantityMeasurementEntity> GetErrorMeasurements()
        {
            return _dbContext.QuantityMeasurements.Where(q => q.HasError).ToList();
        }
        
        public int GetOperationCount(string op)
        {
            return _dbContext.QuantityMeasurements.Count(q => q.OperationType == op);
        }
    }
}
