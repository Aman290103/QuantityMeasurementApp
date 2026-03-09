using System;

namespace QuantityMeasurementApp.Core
{
    // Class representing weight quantity with value and unit
    public class QuantityWeight
    {
        // encapsulated fields
        private readonly double value;

        
        private readonly WeightUnit unit;

        // Constructor to initialize weight value and unit
        public QuantityWeight(double value, WeightUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        // Converts current weight to the target unit
        public double ConvertTo(WeightUnit targetUnit)
        {
            // Validate that value is a valid finite number
            if (!double.IsFinite(this.value))
            {
                throw new ArgumentException("Value must be a finite number.");
            }

            // Convert current value to base unit (Kilogram)
            double baseValue = this.unit.ConvertToBaseUnit(this.value);

            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            // Round result to 5 decimal places
            return Math.Round(convertedValue, 5); 
        }

        // Internal method to handle addition logic
        private QuantityWeight AddInternal(QuantityWeight other, WeightUnit targetUnit)
        {
            // Check for null object
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            // Convert both values to base unit
            double thisBase = this.unit.ConvertToBaseUnit(this.value);
            double otherBase = other.unit.ConvertToBaseUnit(other.value);
            
            
            double sumBase = thisBase + otherBase;

            
            double sumTarget = targetUnit.ConvertFromBaseUnit(sumBase);

            // Return new QuantityWeight object with rounded result
            return new QuantityWeight(Math.Round(sumTarget, 5), targetUnit);
        }

        // Adds another QuantityWeight and keeps current unit
        public QuantityWeight Add(QuantityWeight other)
        {
            return AddInternal(other, this.unit);
        }

        // Adds another QuantityWeight and converts result to target unit
        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            return AddInternal(other, targetUnit);
        }

        // Checks equality by comparing base unit values
        public override bool Equals(object? obj)
        {
            // If both references point to same object
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // If object is null or not of same type
            if (obj == null || obj.GetType() != typeof(QuantityWeight)) 
                return false;

            QuantityWeight other = (QuantityWeight)obj;

            
            double thisBaseValue = Math.Round(this.unit.ConvertToBaseUnit(this.value), 5);
            double otherBaseValue = Math.Round(other.unit.ConvertToBaseUnit(other.value), 5);

            // Compare base values
            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }

        
        public override int GetHashCode()
        {
            return value.GetHashCode() ^ unit.GetHashCode();
        }

        
        public override string ToString()
        {
            return $"{value} {unit}";
        }
    }
}