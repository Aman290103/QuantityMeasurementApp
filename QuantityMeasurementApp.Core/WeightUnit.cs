namespace QuantityMeasurementApp.Core
{
    // Enum representing different weight units
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound
    }

    // Extension methods for WeightUnit enum
    public static class WeightUnitExtensions
    {
        // Returns the conversion factor of each unit to the base unit (Kilogram)
        public static double GetConversionFactor(this WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.Kilogram:
                    return 1.0;        // Base unit
                case WeightUnit.Gram:
                    return 0.001;      // 1 gram = 0.001 kg
                case WeightUnit.Pound:
                    return 0.453592;   // 1 pound = 0.453592 kg
                default:
                    return 1.0;        // Default to kilogram
            }
        }

        // Converts a given value to base unit (Kilogram)
        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        // Converts a base unit (Kilogram) value to the specified unit
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            double factor = unit.GetConversionFactor();

            if (factor == 0) 
                return 0;   // Safety check to avoid division by zero
            
            return baseValue / factor;
        }
    }
}