using System;

namespace QuantityMeasurementApp.Entity
{
    // Generic measurable quantity (Length, Weight, etc.)
    public class QuantityModel<U> where U : IMeasurable
    {
        public double Value { get; }
        public U Unit { get; }

        // Constructor with validation
        public QuantityModel(double value, U unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit), "Unit cannot be null");

            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number.");

            this.Value = value;
            this.Unit = unit;
        }

        // Convert quantity to target unit
        public double ConvertTo(U targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            double baseValue = this.Unit.ConvertToBaseUnit(this.Value);
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
        private void ValidateArithmeticOperands(QuantityModel<U> other, U targetUnit, bool targetUnitRequired)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (targetUnitRequired && targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));
        }

        // Performs arithmetic in base unit
        private double PerformBaseArithmetic(QuantityModel<U> other, ArithmeticOperation operation)
        {
            // UC14: Validate if the unit supports this arithmetic operation
            this.Unit.ValidateOperationSupport(operation.ToString());

            double thisBase = this.Unit.ConvertToBaseUnit(this.Value);
            double otherBase = other.Unit.ConvertToBaseUnit(other.Value);

            switch (operation)
            {
                case ArithmeticOperation.Add:
                    return thisBase + otherBase;
                case ArithmeticOperation.Subtract:
                    return thisBase - otherBase;
                case ArithmeticOperation.Divide:
                    if (Math.Abs(otherBase) < 1e-10)
                    {
                        throw new DivideByZeroException("Cannot divide by a quantity of zero.");
                    }
                    return thisBase / otherBase;
                default:
                    throw new InvalidOperationException("Unsupported arithmetic operation.");
            }
        }

        // -------- Public Arithmetic API --------

        public QuantityModel<U> Add(QuantityModel<U> other)
            => Add(other, this.Unit);

        public QuantityModel<U> Add(QuantityModel<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.Add);
            double resultTarget = targetUnit.ConvertFromBaseUnit(resultBase);

            return new QuantityModel<U>(Math.Round(resultTarget, 5), targetUnit);
        }

        public QuantityModel<U> Subtract(QuantityModel<U> other)
            => Subtract(other, this.Unit);

        public QuantityModel<U> Subtract(QuantityModel<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.Subtract);
            double resultTarget = targetUnit.ConvertFromBaseUnit(resultBase);

            return new QuantityModel<U>(Math.Round(resultTarget, 5), targetUnit);
        }

        // Returns dimensionless result
        public double Divide(QuantityModel<U> other)
        {
            ValidateArithmeticOperands(other, default(U)!, false);
            return PerformBaseArithmetic(other, ArithmeticOperation.Divide);
        }

        // -------- Overrides --------

        // Equality based on base unit comparison
        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || obj.GetType() != typeof(QuantityModel<U>))
                return false;

            QuantityModel<U> other = (QuantityModel<U>)obj;

            double thisBaseValue = Math.Round(this.Unit.ConvertToBaseUnit(this.Value), 5);
            double otherBaseValue = Math.Round(other.Unit.ConvertToBaseUnit(other.Value), 5);

            return thisBaseValue.CompareTo(otherBaseValue) == 0;
        }

        public override int GetHashCode()
            => Value.GetHashCode() ^ Unit.GetHashCode();

        public override string ToString()
            => $"{Value} {Unit.GetUnitName()}";
    }
}
