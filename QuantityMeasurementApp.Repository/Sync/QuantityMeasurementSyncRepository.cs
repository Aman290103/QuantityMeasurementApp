using System;
using System.Collections.Generic;
using System.Linq;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository
{
    // Offline-first repository: always writes to cache JSON first and then tries to push to SQL Server.
    // If SQL Server is unavailable, items are queued in a pending file and retried later.
    public class QuantityMeasurementSyncRepository : IQuantityMeasurementRepository
    {
        private readonly IQuantityMeasurementRepository _cache;
        private readonly IQuantityMeasurementRepository? _database;
        private readonly PendingSyncStore _pendingStore;

        public QuantityMeasurementSyncRepository(
            IQuantityMeasurementRepository cacheRepository,
            IQuantityMeasurementRepository? databaseRepository,
            PendingSyncStore? pendingStore = null)
        {
            _cache = cacheRepository;
            _database = databaseRepository; // allowed to be null (offline mode)
            _pendingStore = pendingStore ?? new PendingSyncStore();
        }

        public void SaveMeasurement(QuantityMeasurementEntity measurement)
        {
            if (measurement == null) throw new ArgumentNullException(nameof(measurement));

            // 1) Always store locally first (offline-first).
            _cache.SaveMeasurement(measurement);

            // 2) Then best-effort push to DB; if it fails, queue for later.
            if (_database == null)
            {
                _pendingStore.Enqueue(measurement);
                return;
            }

            try
            {
                _database.SaveMeasurement(measurement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SyncRepository] DB unavailable, queued operation for later sync: {ex.Message}");
                _pendingStore.Enqueue(measurement);
            }
        }

        public IEnumerable<QuantityMeasurementEntity> GetAllMeasurements() => _cache.GetAllMeasurements();

        public IEnumerable<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType) 
            => _cache.GetMeasurementsByOperation(operationType);

        public IEnumerable<QuantityMeasurementEntity> GetMeasurementsByType(string measurementType)
            => _cache.GetMeasurementsByType(measurementType);

        public int GetTotalCount() => _cache.GetTotalCount();

        public void DeleteAll() => _cache.DeleteAll();

        // Call this at startup (and optionally on exit) to push pending operations to the DB.
        public int SyncPendingToDatabase()
        {
            if (_database == null) return 0;

            var pending = _pendingStore.LoadAll().ToList();
            if (pending.Count == 0) return 0;

            var remaining = new List<QuantityMeasurementEntity>();
            int synced = 0;

            foreach (var item in pending)
            {
                try
                {
                    _database.SaveMeasurement(item);
                    synced++;
                }
                catch
                {
                    remaining.Add(item);
                }
            }

            _pendingStore.OverwriteAll(remaining);
            if (synced > 0)
                Console.WriteLine($"[SyncRepository] Synced {synced} pending operation(s) to SQL Server.");

            return synced;
        }
    }
}
