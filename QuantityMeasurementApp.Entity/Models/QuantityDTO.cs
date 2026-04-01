using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace QuantityMeasurementApp.Entity
{
    public class QuantityDTO : IValidatableObject
    {
        public double Value { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Unit cannot be empty")]
        public string Unit { get; set; } = string.Empty;

        [RegularExpression("^(Length|Volume|Weight|Temperature|Area|Angle|Speed|Time)Unit$", ErrorMessage = "MeasurementType must be LengthUnit, VolumeUnit, WeightUnit, TemperatureUnit, AreaUnit, AngleUnit, SpeedUnit, or TimeUnit.")]
        public string? MeasurementType { get; set; }

        public QuantityDTO() { }

        public QuantityDTO(double value, string unit, string measurementType)
        {
            Value = value;
            Unit = unit;
            MeasurementType = measurementType;
        }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Complex logic can go here. For now, rely on attributes.
            yield break;
        }
    }
}
