using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantityMeasurementApp.Entity
{
    public class QuantityMeasurementDTO
    {
        public double ThisValue { get; set; }
        public string ThisUnit { get; set; } = string.Empty;
        public string ThisMeasurementType { get; set; } = string.Empty;

        public double? ThatValue { get; set; }
        public string? ThatUnit { get; set; }
        public string? ThatMeasurementType { get; set; }

        public string Operation { get; set; } = string.Empty;
        
        public string? ResultString { get; set; }
        public double? ResultValue { get; set; }
        public string? ResultUnit { get; set; }
        public string? ResultMeasurementType { get; set; }
        
        public string? ErrorMessage { get; set; }
        public bool Error { get; set; }

        public static QuantityMeasurementDTO FromEntity(QuantityMeasurementEntity entity)
        {
            return new QuantityMeasurementDTO
            {
                ThisValue = entity.Operand1Value,
                ThisUnit = entity.Operand1Unit,
                ThisMeasurementType = entity.MeasurementType,
                ThatValue = entity.Operand2Value,
                ThatUnit = entity.Operand2Unit,
                Operation = entity.OperationType,
                ResultValue = entity.ResultValue,
                ResultUnit = entity.ResultUnit,
                Error = entity.HasError,
                ErrorMessage = entity.ErrorMessage
            };
        }
        
        public QuantityMeasurementEntity ToEntity()
        {
            return new QuantityMeasurementEntity
            {
                Operand1Value = ThisValue,
                Operand1Unit = ThisUnit,
                MeasurementType = ThisMeasurementType,
                Operand2Value = ThatValue,
                Operand2Unit = ThatUnit,
                OperationType = Operation ?? "UNKNOWN",
                ResultValue = ResultValue,
                ResultUnit = ResultUnit,
                HasError = Error,
                ErrorMessage = ErrorMessage
            };
        }

        public static List<QuantityMeasurementDTO> FromEntityList(List<QuantityMeasurementEntity> entities)
        {
            return entities.Select(FromEntity).ToList();
        }

        public static List<QuantityMeasurementEntity> ToEntityList(List<QuantityMeasurementDTO> dtos)
        {
            return dtos.Select(d => d.ToEntity()).ToList();
        }
    }
}
