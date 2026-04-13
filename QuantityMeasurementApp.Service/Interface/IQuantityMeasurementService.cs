using System.Collections.Generic;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Service
{
    public interface IQuantityMeasurementService
    {
        // Console App methods (backward compatible)
        QuantityDTO Convert(QuantityDTO input, string targetUnit);
        bool Compare(QuantityDTO first, QuantityDTO second);
        QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnit);
        QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnit);
        double Divide(QuantityDTO first, QuantityDTO second);

        // REST API methods (UC17)
        QuantityMeasurementDTO CompareRest(QuantityInputDTO input);
        QuantityMeasurementDTO ConvertRest(QuantityInputDTO input);
        QuantityMeasurementDTO AddRest(QuantityInputDTO input);
        QuantityMeasurementDTO SubtractRest(QuantityInputDTO input);
        QuantityMeasurementDTO DivideRest(QuantityInputDTO input);

        // History methods
        IEnumerable<QuantityMeasurementDTO> GetOperationHistory(string operation);
        IEnumerable<QuantityMeasurementDTO> GetMeasurementsByType(string type);
        int GetOperationCount(string operation);
        IEnumerable<QuantityMeasurementDTO> GetErrorHistory();
        IEnumerable<QuantityMeasurementDTO> GetAllHistory();
        void ClearHistory();
    }
}
