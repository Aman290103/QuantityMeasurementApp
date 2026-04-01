namespace QuantityMeasurementApp.Entity
{
    // Represents different area units and their conversion to base unit (Square Meter)
    public class AreaUnit : IMeasurable
    {
        // Predefined area units
        public static readonly AreaUnit SquareMeter = new AreaUnit("SquareMeter", 1.0);
        public static readonly AreaUnit SquareFoot = new AreaUnit("SquareFoot", 0.092903);
        public static readonly AreaUnit Acre = new AreaUnit("Acre", 4046.86);
        public static readonly AreaUnit SquareInch = new AreaUnit("SquareInch", 0.00064516);

        private readonly string name;
        private readonly double conversionFactor;

        private AreaUnit(string name, double conversionFactor)
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
