using System;

namespace QuantityMeasurementApp.Entity
{
    [Serializable]
    public class QuantityMeasurementEntity
    {
        public string OperationType { get; }
        public string MeasurementType { get; }
        
        public double Operand1Value { get; }
        public string Operand1Unit { get; }
        public double? Operand2Value { get; }
        public string? Operand2Unit { get; }
        
        public double? ResultValue { get; }
        public string? ResultUnit { get; }
        
        public bool HasError { get; }
        public string? ErrorMessage { get; }

        public QuantityMeasurementEntity(string operationType, double op1Val, string op1Unit, 
            double? op2Val, string? op2Unit, double? resVal, string? resUnit, 
            bool hasError = false, string? errorMsg = null, string measurementType = "N/A")
        {
            OperationType = operationType;
            Operand1Value = op1Val;
            Operand1Unit = op1Unit;
            Operand2Value = op2Val;
            Operand2Unit = op2Unit;
            ResultValue = resVal;
            ResultUnit = resUnit;
            HasError = hasError;
            ErrorMessage = errorMsg;
            MeasurementType = measurementType;
        }

        // Single operand success constructor
        public QuantityMeasurementEntity(string operationType, double op1Val, string op1Unit, double resVal, string resUnit)
            : this(operationType, op1Val, op1Unit, null, null, resVal, resUnit, false, null) { }

        // Binary operand success constructor
        public QuantityMeasurementEntity(string operationType, double op1Val, string op1Unit, double op2Val, string op2Unit, double resVal, string resUnit)
            : this(operationType, op1Val, op1Unit, op2Val, op2Unit, resVal, resUnit, false, null) { }

        // Error constructor
        public QuantityMeasurementEntity(string operationType, double op1Val, string op1Unit, double? op2Val, string? op2Unit, string errorMsg)
            : this(operationType, op1Val, op1Unit, op2Val, op2Unit, null, null, true, errorMsg) { }

        public override string ToString()
        {
            if (HasError)
                return $"[ERROR] Operation: {OperationType} failed: {ErrorMessage}";
            return $"[{OperationType}] {Operand1Value} {Operand1Unit} " +
                   (Operand2Value.HasValue ? $"and {Operand2Value} {Operand2Unit} " : "") +
                   $"-> Result: {ResultValue} {ResultUnit}";
        }
    }
}
