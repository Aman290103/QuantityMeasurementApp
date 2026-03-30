using System;
using Microsoft.Data.SqlClient;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository
{
    public static class QuantityMeasurementRowMapper
    {
        public static QuantityMeasurementEntity MapRow(SqlDataReader reader)
        {
            string opType = reader["OperationType"].ToString() ?? "";
            bool hasError = Convert.ToBoolean(reader["HasError"]);
            string errMsg = reader["ErrorMessage"].ToString() ?? "None";
            string measType = reader["MeasurementType"].ToString() ?? "N/A";
            
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

            return new QuantityMeasurementEntity(
                opType, op1val, op1unit, op2val, op2unit, resval, resunit, hasError, errMsg, measType
            );
        }

        private static void ParseOperand(string op, out double val, out string unit)
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
