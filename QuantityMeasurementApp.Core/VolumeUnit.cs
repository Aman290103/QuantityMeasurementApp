namespace QuantityMeasurementApp.Core
{
    // Represents volume units with base unit as Litre
    public class VolumeUnit : IMeasurable
    {
        // Predefined volume units
        public static readonly VolumeUnit Litre = new VolumeUnit("Litre", 1.0);
        public static readonly VolumeUnit Millilitre = new VolumeUnit("Millilitre", 0.001);
        public static readonly VolumeUnit Gallon = new VolumeUnit("Gallon", 3.78541);

        private readonly string name;
        private readonly double conversionFactor;

        // Private constructor to restrict object creation
        private VolumeUnit(string name, double conversionFactor)
        {
            this.name = name;
            this.conversionFactor = conversionFactor;
        }

        // Returns conversion factor to base unit
        public double GetConversionFactor() => conversionFactor;

        // Converts value to base unit (Litre)
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