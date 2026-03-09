using System;

namespace QuantityMeasurementApp.Core
{
    // Generic measurable quantity (Length, Weight, etc.)
    public class Quantity<T> where T : IMeasurable
    {
        private readonly double value; // numeric value
        private readonly T unit;       // unit of measurement

        // Constructor with validation
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

        // Centralized arithmetic operations
        public enum ArithmeticOperation
        {
            Add,
            Subtract,
            Divide
        }

        // Common validation for arithmetic methods
        private void ValidateArithmeticOperands(Quantity<T> other, T targetUnit, bool targetUnitRequired)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (targetUnitRequired && targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));
        }

        // Performs arithmetic in base unit
        private double PerformBaseArithmetic(Quantity<T> other, ArithmeticOperation operation)
        {
            double thisBase = this.unit.ConvertToBaseUnit(this.value);
            double otherBase = other.unit.ConvertToBaseUnit(other.value);

            switch (operation)
            {
                case ArithmeticOperation.Add:
                    return thisBase + otherBase;

                case ArithmeticOperation.Subtract:
                    return thisBase - otherBase;

                case ArithmeticOperation.Divide:
                    if (Math.Abs(otherBase) < 1e-10)
                        throw new DivideByZeroException("Cannot divide by zero quantity.");
                    return thisBase / otherBase;

                default:
                    throw new InvalidOperationException("Unsupported operation.");
            }
        }

        // -------- Public Arithmetic API --------

        public Quantity<T> Add(Quantity<T> other)
            => Add(other, this.unit);

        public Quantity<T> Add(Quantity<T> other, T targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.Add);
            double resultTarget = targetUnit.ConvertFromBaseUnit(resultBase);

            return new Quantity<T>(Math.Round(resultTarget, 5), targetUnit);
        }

        public Quantity<T> Subtract(Quantity<T> other)
            => Subtract(other, this.unit);

        public Quantity<T> Subtract(Quantity<T> other, T targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.Subtract);
            double resultTarget = targetUnit.ConvertFromBaseUnit(resultBase);

            return new Quantity<T>(Math.Round(resultTarget, 5), targetUnit);
        }

        // Returns dimensionless result
        public double Divide(Quantity<T> other)
        {
            ValidateArithmeticOperands(other, default(T)!, false);
            return PerformBaseArithmetic(other, ArithmeticOperation.Divide);
        }

        // -------- Overrides --------

        // Equality based on base unit comparison
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
            => value.GetHashCode() ^ unit.GetHashCode();

        public override string ToString()
            => $"{value} {unit.GetUnitName()}";
    }
}