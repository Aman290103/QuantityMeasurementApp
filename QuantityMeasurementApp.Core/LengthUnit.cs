namespace QuantityMeasurementApp.Core
{
    // Represents different length units and their conversion to base unit (Feet)
    public class LengthUnit : IMeasurable
    {
        // Predefined length units
        public static readonly LengthUnit Feet = new LengthUnit("Feet", 1.0);
        public static readonly LengthUnit Inch = new LengthUnit("Inch", 1.0 / 12.0);
        public static readonly LengthUnit Yard = new LengthUnit("Yard", 3.0);
        public static readonly LengthUnit Centimeter = new LengthUnit("Centimeter", 1.0 / 30.48);

        private readonly string name;
        private readonly double conversionFactor;

        // Private constructor to restrict external creation
        private LengthUnit(string name, double conversionFactor)
        {
            this.name = name;
            this.conversionFactor = conversionFactor;
        }

        // Returns conversion factor to base unit
        public double GetConversionFactor() => conversionFactor;

        // Converts given value to base unit (Feet)
        public double ConvertToBaseUnit(double value) => value * conversionFactor;

        // Converts base unit value back to this unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            if (conversionFactor == 0) return 0;
            return baseValue / conversionFactor;
        }

        public string GetUnitName() => name;

        public override string ToString() => name;
    }
}