namespace QuantityMeasurementApp.Entity
{
    // Represents different speed units and their conversion to base unit (m/s)
    public class SpeedUnit : IMeasurable
    {
        // Predefined speed units
        public static readonly SpeedUnit MetersPerSecond = new SpeedUnit("MetersPerSecond", 1.0);
        public static readonly SpeedUnit KilometersPerHour = new SpeedUnit("KilometersPerHour", 1.0 / 3.6);
        public static readonly SpeedUnit MilesPerHour = new SpeedUnit("MilesPerHour", 0.44704);
        public static readonly SpeedUnit Knots = new SpeedUnit("Knots", 0.514444);

        private readonly string name;
        private readonly double conversionFactor;

        private SpeedUnit(string name, double conversionFactor)
        {
            this.name = name;
            this.conversionFactor = conversionFactor;
        }

        public double GetConversionFactor() => conversionFactor;

        public double ConvertToBaseUnit(double value) => value * conversionFactor;

        public double ConvertFromBaseUnit(double baseValue)
        {
            if (conversionFactor == 0) return 0;
            return baseValue / conversionFactor;
        }

        public string GetUnitName() => name;

        public override string ToString() => name;
    }
}
