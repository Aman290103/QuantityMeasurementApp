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

            double thisBaseValue = Math.Round(this.unit.ConvertToBaseUnit(this.value), 5);
            double otherBaseValue = Math.Round(other.unit.ConvertToBaseUnit(other.value), 5);

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
            if (!double.IsFinite(this.value))
            {
                throw new ArgumentException("Value must be a finite number.");
            }

            double baseValue = this.unit.ConvertToBaseUnit(this.value);
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

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

            double thisBase = this.unit.ConvertToBaseUnit(this.value);
            double otherBase = other.unit.ConvertToBaseUnit(other.value);
            
            double sumBase = thisBase + otherBase;
            double sumTarget = targetUnit.ConvertFromBaseUnit(sumBase);
            
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