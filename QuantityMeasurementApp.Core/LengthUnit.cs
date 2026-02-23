namespace QuantityMeasurementApp.Core
{
    public enum LengthUnit
    {
        Feet,
        Inch,
        Yard,
        Centimeter
    }

    public static class LengthUnitExtensions
    {
        public static double GetBaseValue(this LengthUnit unit, double value)
        {
            if (unit == LengthUnit.Feet)
            {
                return value * 12.0;
            }
            if (unit == LengthUnit.Yard)
            {
                return value * 36.0;
            }
            if (unit == LengthUnit.Centimeter)
            {
                return value * 0.393701;
            }
            
            return value; 
        }
    }
}