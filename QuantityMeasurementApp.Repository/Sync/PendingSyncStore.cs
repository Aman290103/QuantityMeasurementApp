using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository
{
    // Stores operations that were recorded locally but not yet pushed to SQL Server.
    public class PendingSyncStore
    {
        private readonly string _pendingFilePath;
        private static readonly object LockObject = new object();

        public PendingSyncStore(string pendingFilePath = "measurement_pending_sync.json")
        {
            _pendingFilePath = pendingFilePath;
        }

        public void Enqueue(QuantityMeasurementEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            lock (LockObject)
            {
                var all = LoadUnsafe();
                all.Add(entity);
                SaveUnsafe(all);
            }
        }

        public IReadOnlyList<QuantityMeasurementEntity> LoadAll()
        {
            lock (LockObject)
            {
                return LoadUnsafe();
            }
        }

        public void OverwriteAll(List<QuantityMeasurementEntity> entities)
        {
            lock (LockObject)
            {
                SaveUnsafe(new List<QuantityMeasurementEntity>(entities));
            }
        }

        private List<QuantityMeasurementEntity> LoadUnsafe()
        {
            try
            {
                if (!File.Exists(_pendingFilePath))
                    return new List<QuantityMeasurementEntity>();

                string json = File.ReadAllText(_pendingFilePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<QuantityMeasurementEntity>();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(json, options)
                       ?? new List<QuantityMeasurementEntity>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading pending sync file: {ex.Message}");
                return new List<QuantityMeasurementEntity>();
            }
        }

        private void SaveUnsafe(List<QuantityMeasurementEntity> entities)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(entities, options);
                File.WriteAllText(_pendingFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving pending sync file: {ex.Message}");
            }
        }
    }
}
