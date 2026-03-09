using System;

namespace QuantityMeasurementApp.Core
{
    public class TemperatureUnit : IMeasurable
    {
        // Base Unit: Celsius
        public static readonly TemperatureUnit Celsius = new TemperatureUnit(
            "Celsius", 
            c => c, 
            c => c);
            
        public static readonly TemperatureUnit Fahrenheit = new TemperatureUnit(
            "Fahrenheit", 
            f => (f - 32.0) * 5.0 / 9.0, 
            c => (c * 9.0 / 5.0) + 32.0);
            
        public static readonly TemperatureUnit Kelvin = new TemperatureUnit(
            "Kelvin", 
            k => k - 273.15, 
            c => c + 273.15);

        private readonly string name;
        private readonly Func<double, double> toBase;
        private readonly Func<double, double> fromBase;

        private TemperatureUnit(string name, Func<double, double> toBase, Func<double, double> fromBase)
        {
            this.name = name;
            this.toBase = toBase;
            this.fromBase = fromBase;
        }

        // Dummy conversion factor since temperature uses non-linear lambda functions
        public double GetConversionFactor() => 1.0; 

        public double ConvertToBaseUnit(double value) => toBase(value);

        public double ConvertFromBaseUnit(double baseValue) => fromBase(baseValue);

        public string GetUnitName() => name;

        // UC14: Opt-out of Arithmetic Operations
        public bool SupportsArithmetic() => false;

        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException($"Temperature does not support {operation} operations.");
        }

        public override string ToString() => name;
    }
}