using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Service
{
    public interface IQuantityMeasurementService
    {
        QuantityDTO Convert(QuantityDTO input, string targetUnit);
        bool Compare(QuantityDTO first, QuantityDTO second);
        QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnit);
        QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnit);
        double Divide(QuantityDTO first, QuantityDTO second);
    }
}
