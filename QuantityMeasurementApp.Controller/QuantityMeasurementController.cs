using System;
using QuantityMeasurementApp.Entity;
using QuantityMeasurementApp.Service;

namespace QuantityMeasurementApp.Controller
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        public void PerformComparison(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                bool result = _service.Compare(first, second);
                Console.WriteLine($"Comparison: {first.Value} {first.Unit} equals {second.Value} {second.Unit} -> Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error in Comparison] {ex.Message}");
            }
        }

        public void PerformConversion(QuantityDTO input, string targetUnit)
        {
            try
            {
                var result = _service.Convert(input, targetUnit);
                Console.WriteLine($"Conversion: {input.Value} {input.Unit} converted to {targetUnit} -> Result: {result.Value} {result.Unit}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error in Conversion] {ex.Message}");
            }
        }

        public void PerformAddition(QuantityDTO first, QuantityDTO second, string targetUnit)
        {
            try
            {
                var result = _service.Add(first, second, targetUnit);
                Console.WriteLine($"Addition: {first.Value} {first.Unit} + {second.Value} {second.Unit} in {targetUnit} -> Result: {result.Value} {result.Unit}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error in Addition] {ex.Message}");
            }
        }

        public void PerformSubtraction(QuantityDTO first, QuantityDTO second, string targetUnit)
        {
             try
            {
                var result = _service.Subtract(first, second, targetUnit);
                Console.WriteLine($"Subtraction: {first.Value} {first.Unit} - {second.Value} {second.Unit} in {targetUnit} -> Result: {result.Value} {result.Unit}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error in Subtraction] {ex.Message}");
            }
        }

        public void PerformDivision(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                double result = _service.Divide(first, second);
                Console.WriteLine($"Division: {first.Value} {first.Unit} / {second.Value} {second.Unit} -> Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error in Division] {ex.Message}");
            }
        }
    }
}
