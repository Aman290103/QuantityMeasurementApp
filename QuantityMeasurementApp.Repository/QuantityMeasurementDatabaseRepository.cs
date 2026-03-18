using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Repository
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly string _connectionString;

        public QuantityMeasurementDatabaseRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be empty", nameof(connectionString));
                
            _connectionString = connectionString;
            InitializeSchema();
        }

        private void InitializeSchema()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                
                // Simple verify table exists
                string checkQuery = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'QuantityMeasurements'";
                using var command = new SqlCommand(checkQuery, connection);
                var result = command.ExecuteScalar();
                
                if (result == null)
                {
                    // Table doesn't exist. Usually we'd create it here or warn the user.
                    // Based on requirements, the user must run schema.sql in SSMS.
                    Console.WriteLine("WARNING: QuantityMeasurements table not found. Please run schema.sql in SSMS!");
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to initialize database connection. Ensure the connection string is correct and schema.sql was run.", ex);
            }
        }

        public void SaveMeasurement(QuantityMeasurementEntity entity)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string query = @"
                    INSERT INTO QuantityMeasurements 
                    (FirstOperand, SecondOperand, OperationType, MeasurementType, FinalResult, HasError, ErrorMessage)
                    VALUES (@FirstOperand, @SecondOperand, @OperationType, @MeasurementType, @FinalResult, @HasError, @ErrorMessage)";

                using var command = new SqlCommand(query, connection);
                
                string firstOp = $"{entity.Operand1Value} {entity.Operand1Unit}";
                string secondOp = entity.Operand2Value.HasValue ? $"{entity.Operand2Value} {entity.Operand2Unit}" : "N/A";
                string resultStr = entity.ResultValue.HasValue ? $"{entity.ResultValue} {entity.ResultUnit}" : "N/A";

                command.Parameters.AddWithValue("@FirstOperand", firstOp);
                command.Parameters.AddWithValue("@SecondOperand", secondOp);
                command.Parameters.AddWithValue("@OperationType", entity.OperationType);
                command.Parameters.AddWithValue("@MeasurementType", entity.MeasurementType);
                command.Parameters.AddWithValue("@FinalResult", resultStr);
                command.Parameters.AddWithValue("@HasError", entity.HasError);
                command.Parameters.AddWithValue("@ErrorMessage", string.IsNullOrEmpty(entity.ErrorMessage) ? "None" : entity.ErrorMessage);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to save measurement to database", ex);
            }
        }

        public IEnumerable<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return ExecuteFetchQuery("SELECT * FROM QuantityMeasurements ORDER BY RecordedAt ASC");
        }

        public IEnumerable<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            string query = "SELECT * FROM QuantityMeasurements WHERE OperationType = @OpType ORDER BY RecordedAt ASC";
            return ExecuteFetchQuery(query, cmd => cmd.Parameters.AddWithValue("@OpType", operationType));
        }

        public IEnumerable<QuantityMeasurementEntity> GetMeasurementsByType(string measurementType)
        {
            string query = "SELECT * FROM QuantityMeasurements WHERE MeasurementType = @MeasType ORDER BY RecordedAt ASC";
            return ExecuteFetchQuery(query, cmd => cmd.Parameters.AddWithValue("@MeasType", measurementType));
        }

        public int GetTotalCount()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                string query = "SELECT COUNT(*) FROM QuantityMeasurements";
                using var command = new SqlCommand(query, connection);
                return (int)command.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to get total count", ex);
            }
        }

        public void DeleteAll()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                string query = "DELETE FROM QuantityMeasurements";
                using var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to delete all measurements", ex);
            }
        }

        private IEnumerable<QuantityMeasurementEntity> ExecuteFetchQuery(string query, Action<SqlCommand>? configCommand = null)
        {
            var results = new List<QuantityMeasurementEntity>();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = new SqlCommand(query, connection);
                configCommand?.Invoke(command);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Since the database stores the combined strings "10 Feet", we need to parse them back roughly,
                    // or construct an entity directly if it is just for display.
                    // For now, let's load what we can for backward compatibility on display.
                    string opType = reader["OperationType"].ToString() ?? "";
                    bool hasError = Convert.ToBoolean(reader["HasError"]);
                    string errMsg = reader["ErrorMessage"].ToString() ?? "None";
                    string measType = reader["MeasurementType"].ToString() ?? "N/A";
                    
                    // We parse the first operand safely (ignoring rigorous checks for display) 
                    double op1val = 0; string op1unit = "Unknown";
                    double? op2val = null; string? op2unit = null;
                    double? resval = null; string? resunit = null;

                    ParseOperand(reader["FirstOperand"].ToString() ?? "", out op1val, out op1unit);
                    
                    if (reader["SecondOperand"].ToString() != "N/A")
                    {
                        ParseOperand(reader["SecondOperand"].ToString() ?? "", out double v, out string u);
                        op2val = v; op2unit = u;
                    }

                    if (reader["FinalResult"].ToString() != "N/A")
                    {
                        ParseOperand(reader["FinalResult"].ToString() ?? "", out double v, out string u);
                        resval = v; resunit = u;
                    }

                    var entity = new QuantityMeasurementEntity(
                        opType, op1val, op1unit, op2val, op2unit, resval, resunit, hasError, errMsg, measType
                    );
                    
                    results.Add(entity);
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Failed to fetch measurements from database", ex);
            }
            return results;
        }

        private void ParseOperand(string op, out double val, out string unit)
        {
            val = 0;
            unit = "Unknown";
            if (string.IsNullOrWhiteSpace(op)) return;

            var parts = op.Trim().Split(' ', 2);
            if (parts.Length > 0 && double.TryParse(parts[0], out double v))
                val = v;
            if (parts.Length > 1)
                unit = parts[1];
        }
    }
}
