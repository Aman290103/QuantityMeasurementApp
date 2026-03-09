namespace QuantityMeasurementApp.Core
{
    // Represents weight units with base unit as Kilogram
    public class WeightUnit : IMeasurable
    {
        // Predefined weight units
        public static readonly WeightUnit Kilogram = new WeightUnit("Kilogram", 1.0);
        public static readonly WeightUnit Gram = new WeightUnit("Gram", 0.001);
        public static readonly WeightUnit Pound = new WeightUnit("Pound", 0.453592);

        private readonly string name;
        private readonly double conversionFactor;

        // Private constructor to prevent external instantiation
        private WeightUnit(string name, double conversionFactor)
        {
            this.name = name;
            this.conversionFactor = conversionFactor;
        }

        // Returns conversion factor to base unit
        public double GetConversionFactor() => conversionFactor;

        // Converts value to base unit (Kilogram)
        public double ConvertToBaseUnit(double value) => value * conversionFactor;

        // Converts base unit value to this unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            if (conversionFactor == 0) return 0;
            return baseValue / conversionFactor;
        }

        public string GetUnitName() => name;

        public override string ToString() => name;
    }
}