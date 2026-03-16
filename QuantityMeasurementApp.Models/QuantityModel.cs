using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.Models
{
    public class QuantityModel<T> where T : IMeasurable
    {
        public double Value { get; set; }
        public T Unit { get; set; }

        public QuantityModel(double value, T unit)
        {
            Value = value;
            Unit = unit;
        }

        // Helper to integrate with Core logic seamlessly
        public Quantity<T> ToQuantity()
        {
            return new Quantity<T>(Value, Unit);
        }
    }
}
