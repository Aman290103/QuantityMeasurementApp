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
        /// <summary>
        /// Returns the value converted to the base unit (Inches).
        /// </summary>
        public static double GetBaseValue(this LengthUnit unit, double value)
        {
            switch (unit)
            {
                case LengthUnit.Feet:
                    return value * 12.0;
                case LengthUnit.Yard:
                    return value * 36.0;
                case LengthUnit.Centimeter:
                    return value * 0.393701;
                case LengthUnit.Inch:
                    return value * 1.0;
                default:
                    return value;
            }
        }

        /// <summary>
        /// Gets the conversion factor relative to the base unit (Inches).
        /// </summary>
        public static double GetConversionFactor(this LengthUnit unit)
        {
            return unit.GetBaseValue(1.0);
        }
    }
}