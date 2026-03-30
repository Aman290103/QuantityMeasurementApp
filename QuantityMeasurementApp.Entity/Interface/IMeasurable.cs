namespace QuantityMeasurementApp.Entity
{
    // Interface for measurement units
    public interface IMeasurable
    {
        // Returns conversion factor to base unit
        double GetConversionFactor();

        // Converts given value to base unit
        double ConvertToBaseUnit(double value);

        // Converts base unit value back to current unit
        double ConvertFromBaseUnit(double baseValue);

        // Returns unit name
        string GetUnitName();

        // UC14: Optional Operation Support (Default interface methods)
        bool SupportsArithmetic() => true;

        void ValidateOperationSupport(string operation)
        {
            // Default implementation allows operations. 
            // Subclasses can override to throw exceptions for unsupported operations.
        }
    }
}
