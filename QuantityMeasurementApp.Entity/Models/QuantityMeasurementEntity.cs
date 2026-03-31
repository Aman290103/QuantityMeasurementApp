using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementApp.Entity
{
    [Table("QuantityMeasurements")]
    public class QuantityMeasurementEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("OperationType")]
        public string OperationType { get; set; } = string.Empty;

        [Column("MeasurementType")]
        public string MeasurementType { get; set; } = string.Empty;
        
        [Column("FirstOperand")]
        public string Operand1ValueWithUnit { get; set; } = string.Empty;
        
        [NotMapped]
        public double Operand1Value { get; set; }
        [NotMapped]
        public string Operand1Unit { get; set; } = string.Empty;

        [Column("SecondOperand")]
        public string? Operand2ValueWithUnit { get; set; }

        [NotMapped]
        public double? Operand2Value { get; set; }
        [NotMapped]
        public string? Operand2Unit { get; set; }
        
        [Column("FinalResult")]
        public string ResultString { get; set; } = string.Empty;

        [NotMapped]
        public double? ResultValue { get; set; }
        [NotMapped]
        public string? ResultUnit { get; set; }
        
        [Column("HasError")]
        public bool HasError { get; set; }

        [Column("ErrorMessage")]
        public string? ErrorMessage { get; set; }
        
        [Column("RecordedAt")]
        public DateTime CreatedAt { get; set; }

        public QuantityMeasurementEntity() 
        {
            CreatedAt = DateTime.UtcNow;
        }

        public QuantityMeasurementEntity(string operationType, double op1Val, string op1Unit, 
            double? op2Val, string? op2Unit, double? resVal, string? resUnit, 
            bool hasError = false, string? errorMsg = null, string measurementType = "N/A")
            : this()
        {
            OperationType = operationType;
            Operand1Value = op1Val;
            Operand1Unit = op1Unit;
            Operand1ValueWithUnit = $"{op1Val} {op1Unit}";
            
            Operand2Value = op2Val;
            Operand2Unit = op2Unit;
            Operand2ValueWithUnit = op2Val.HasValue ? $"{op2Val} {op2Unit}" : "N/A";
            
            ResultValue = resVal;
            ResultUnit = resUnit;
            ResultString = resVal.HasValue ? $"{resVal} {resUnit}" : "FAILED";
            
            HasError = hasError;
            ErrorMessage = errorMsg ?? "None";
            MeasurementType = measurementType;
        }
        
        public QuantityMeasurementEntity(string operationType, double op1Val, string op1Unit, double resVal, string resUnit)
            : this(operationType, op1Val, op1Unit, null, null, resVal, resUnit, false, null) { }

        public QuantityMeasurementEntity(string operationType, double op1Val, string op1Unit, double op2Val, string op2Unit, double resVal, string resUnit)
            : this(operationType, op1Val, op1Unit, op2Val, op2Unit, resVal, resUnit, false, null) { }

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
