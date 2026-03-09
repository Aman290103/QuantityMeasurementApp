using System;

namespace QuantityMeasurementApp.Core
{
    // Generic class to represent measurable quantities (Length, Weight, etc.)
    public class Quantity<T> where T : IMeasurable
    {
        private readonly double value;
        private readonly T unit;

        // Constructor with basic validation
        public Quantity(double value, T unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit), "Unit cannot be null");

            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number.");

            this.value = value;
            this.unit = unit;
        }

        // Convert quantity to target unit
        public double ConvertTo(T targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            double baseValue = this.unit.ConvertToBaseUnit(this.value);
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return Math.Round(convertedValue, 5);
        }

        // Internal logic for addition
        private Quantity<T> AddInternal(Quantity<T> other, T targetUnit)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            double thisBase = this.unit.ConvertToBaseUnit(this.value);
            double otherBase = other.unit.ConvertToBaseUnit(other.value);

            double sumBase = thisBase + otherBase;
            double sumTarget = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Quantity<T>(Math.Round(sumTarget, 5), targetUnit);
        }

        // Add and return result in current unit
        public Quantity<T> Add(Quantity<T> other)
        {
            return AddInternal(other, this.unit);
        }

        // Add and return result in specified unit
        public Quantity<T> Add(Quantity<T> other, T targetUnit)
        {
            return AddInternal(other, targetUnit);
        }

        // Checks equality by comparing base unit values
        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != typeof(Quantity<T>))
                return false;

            Quantity<T> other = (Quantity<T>)obj;

            double thisBaseValue = Math.Round(this.unit.ConvertToBaseUnit(this.value), 5);
            double otherBaseValue = Math.Round(other.unit.ConvertToBaseUnit(other.value), 5);

            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() ^ unit.GetHashCode();
        }

        public override string ToString()
        {
            return $"{value} {unit.GetUnitName()}";
        }
    }
}