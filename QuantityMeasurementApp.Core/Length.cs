using System;

namespace QuantityMeasurementApp.Core
{
    public class QuantityLength
    {
        // making field encapsulated
        private readonly double value;
        private readonly LengthUnit unit;

        // constructor
        public QuantityLength(double value, LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }


        // overriding the Equals method to handle edge cases
        public override bool Equals(object? obj)
        {
            // checking for same reference
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // checking for object matches type and not null
            if (obj == null || obj.GetType() != typeof(QuantityLength)) return false;

            QuantityLength other = (QuantityLength)obj;

            double thisBaseValue = this.unit.GetBaseValue(this.value);
            double otherBaseValue = other.unit.GetBaseValue(other.value);

            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }

        // comparing the actual value 
        public override int GetHashCode()
        {
            return value.GetHashCode() ^ unit.GetHashCode();
        }


        /// <summary>
        /// Converts the current quantity to a target unit and returns the numeric value.
        /// </summary>
        public double ConvertTo(LengthUnit targetUnit)
        {
            // 1. Validate Input
            if (!double.IsFinite(this.value))
            {
                throw new ArgumentException("Value must be a finite number.");
            }

            // 2. Convert to Base Unit (Inches)
            double baseValue = this.unit.GetBaseValue(this.value);

            // 3. Convert from Base Unit to Target Unit
            double targetFactor = targetUnit.GetConversionFactor();

            // Avoid division by zero if factor is 0 (unlikely for length, but good practice)
            if (targetFactor == 0) return 0;

            double convertedValue = baseValue / targetFactor;

            // 4. Return result (rounding can be handled by caller or here if strict equality needed)
            return Math.Round(convertedValue, 5);
        }


        public override string ToString()
        {
            return $"{value} {unit}";
        }


        //methods for addition
        private QuantityLength AddInternal(QuantityLength other, LengthUnit targetUnit)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            double thisBase = this.unit.GetBaseValue(this.value);
            double otherBase = other.unit.GetBaseValue(other.value);
            
            double sumBase = thisBase + otherBase;
            double targetFactor = targetUnit.GetConversionFactor();
            
            double sumTarget = targetFactor == 0 ? 0 : sumBase / targetFactor;

            return new QuantityLength(Math.Round(sumTarget, 5), targetUnit);
        }

        public QuantityLength Add(QuantityLength other)
        {
            return AddInternal(other, this.unit);
        }

        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            return AddInternal(other, targetUnit);
        }

        public static QuantityLength Add(QuantityLength first, QuantityLength second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            
            return first.Add(second);
        }

        public static QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            
            return first.Add(second, targetUnit);
        }
    }
}