using System;
using QuantityMeasurementApp.Entity;
using QuantityMeasurementApp.Repository;

namespace QuantityMeasurementApp.Service
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementService(IQuantityMeasurementRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private QuantityModel<IMeasurable> ExtractQuantityModel(QuantityDTO dto)
        {
            var unit = UnitMapper.GetUnitFromString(dto.Unit, dto.MeasurementType);
            return new QuantityModel<IMeasurable>(dto.Value, unit);
        }

        public QuantityDTO Convert(QuantityDTO input, string targetUnit)
        {
            try
            {
                var inputModel = ExtractQuantityModel(input);
                var targetUnitObj = UnitMapper.GetUnitFromString(targetUnit, input.MeasurementType);
                
                double resultValue = inputModel.ConvertTo(targetUnitObj);

                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "CONVERT",
                    input.Value, input.Unit,
                    resultValue, targetUnit));

                return new QuantityDTO(resultValue, targetUnit, input.MeasurementType);
            }
            catch (Exception ex)
            {
                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "CONVERT",
                    input.Value, input.Unit,
                    null, null,
                    ex.Message));
                throw new QuantityMeasurementException($"Conversion failed: {ex.Message}", ex);
            }
        }

        public bool Compare(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                if (!first.MeasurementType.Equals(second.MeasurementType, StringComparison.OrdinalIgnoreCase))
                {
                    throw new QuantityMeasurementException("Measurement categories do not match.");
                }

                var q1 = ExtractQuantityModel(first);
                var q2 = ExtractQuantityModel(second);

                bool result = q1.Equals(q2);

                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "COMPARE",
                    first.Value, first.Unit,
                    second.Value, second.Unit,
                    result ? 1 : 0, "Boolean"));

                return result;
            }
            catch (Exception ex) when (ex is not QuantityMeasurementException)
            {
                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "COMPARE",
                    first.Value, first.Unit,
                    second.Value, second.Unit,
                    ex.Message));
                throw new QuantityMeasurementException($"Comparison failed: {ex.Message}", ex);
            }
        }

        public QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnit)
        {
            return PerformArithmetic("ADD", first, second, targetUnit, QuantityModel<IMeasurable>.ArithmeticOperation.Add);
        }

        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnit)
        {
            return PerformArithmetic("SUBTRACT", first, second, targetUnit, QuantityModel<IMeasurable>.ArithmeticOperation.Subtract);
        }

        public double Divide(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                if (!first.MeasurementType.Equals(second.MeasurementType, StringComparison.OrdinalIgnoreCase))
                {
                    throw new QuantityMeasurementException("Measurement categories do not match.");
                }

                var q1 = ExtractQuantityModel(first);
                var q2 = ExtractQuantityModel(second);

                double result = q1.Divide(q2);

                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "DIVIDE",
                    first.Value, first.Unit,
                    second.Value, second.Unit,
                    result, "Scalar"));

                return result;
            }
            catch (Exception ex)
            {
                 _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "DIVIDE",
                    first.Value, first.Unit,
                    second.Value, second.Unit,
                    ex.Message));
                throw new QuantityMeasurementException($"Division failed: {ex.Message}", ex);
            }
        }

        private QuantityDTO PerformArithmetic(string opName, QuantityDTO first, QuantityDTO second, string targetUnit, QuantityModel<IMeasurable>.ArithmeticOperation operation)
        {
            try
            {
                if (!first.MeasurementType.Equals(second.MeasurementType, StringComparison.OrdinalIgnoreCase))
                {
                    throw new QuantityMeasurementException("Measurement categories do not match.");
                }

                var q1 = ExtractQuantityModel(first);
                var q2 = ExtractQuantityModel(second);
                var targetUnitObj = UnitMapper.GetUnitFromString(targetUnit, first.MeasurementType);

                QuantityModel<IMeasurable> resultQ;
                
                if (operation == QuantityModel<IMeasurable>.ArithmeticOperation.Add)
                    resultQ = q1.Add(q2, targetUnitObj);
                else
                    resultQ = q1.Subtract(q2, targetUnitObj);

                double resultValue = resultQ.ConvertTo(targetUnitObj);

                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    opName,
                    first.Value, first.Unit,
                    second.Value, second.Unit,
                    resultValue, targetUnit));

                return new QuantityDTO(resultValue, targetUnit, first.MeasurementType);
            }
            catch (Exception ex) when (ex is not QuantityMeasurementException)
            {
                 _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    opName,
                    first.Value, first.Unit,
                    second.Value, second.Unit,
                    ex.Message));
                throw new QuantityMeasurementException($"{opName} failed: {ex.Message}", ex);
            }
        }
    }
}
