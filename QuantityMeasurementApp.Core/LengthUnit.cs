namespace QuantityMeasurementApp.Core
{
    public enum LengthUnit
    {
        Feet,
        Inch
    }

    public static class LengthUnitExtensions
    {
        public static double GetBaseValue(this LengthUnit unit, double value)
        {
            if (unit == LengthUnit.Feet)
            {
                return value * 12.0;
            }
            
            return value * 1.0;
        }
    }
}