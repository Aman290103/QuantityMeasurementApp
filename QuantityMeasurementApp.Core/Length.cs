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
    }
}