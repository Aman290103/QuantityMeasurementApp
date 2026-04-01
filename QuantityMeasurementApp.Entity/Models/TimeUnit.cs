namespace QuantityMeasurementApp.Entity
{
    // Represents different time units and their conversion to base unit (Second)
    public class TimeUnit : IMeasurable
    {
        // Predefined time units
        public static readonly TimeUnit Second = new TimeUnit("Second", 1.0);
        public static readonly TimeUnit Minute = new TimeUnit("Minute", 60.0);
        public static readonly TimeUnit Hour = new TimeUnit("Hour", 3600.0);
        public static readonly TimeUnit Day = new TimeUnit("Day", 86400.0);

        private readonly string name;
        private readonly double conversionFactor;

        private TimeUnit(string name, double conversionFactor)
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
