using System;
using System.Collections.Generic;
using System.Linq;
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

        private QuantityModel<IMeasurable> ConvertDtoToModel(QuantityDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MeasurementType))
                throw new ArgumentException("Measurement type is required for base quantity.");
            var unit = UnitMapper.GetUnitFromString(dto.Unit, dto.MeasurementType);
            return new QuantityModel<IMeasurable>(dto.Value, unit);
        }

        // ==================== Console App Methods (Backward Compatible) ====================

        public QuantityDTO Convert(QuantityDTO input, string targetUnit)
        {
            try
            {
                var inputModel = ConvertDtoToModel(input);
                var targetUnitObj = UnitMapper.GetUnitFromString(targetUnit, input.MeasurementType ?? "N/A");
                double resultValue = inputModel.ConvertTo(targetUnitObj);

                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "CONVERT", input.Value, input.Unit, null, null, resultValue, targetUnit, false, null, input.MeasurementType ?? "N/A"));

                return new QuantityDTO(resultValue, targetUnit, input.MeasurementType ?? "N/A");
            }
            catch (Exception ex)
            {
                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "CONVERT", input.Value, input.Unit, null, null, ex.Message));
                throw new QuantityMeasurementException($"Conversion failed: {ex.Message}", ex);
            }
        }

        public bool Compare(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                if (!string.Equals(first.MeasurementType, second.MeasurementType, StringComparison.OrdinalIgnoreCase))
                    throw new QuantityMeasurementException("Measurement categories do not match.");

                var q1 = ConvertDtoToModel(first);
                var q2 = ConvertDtoToModel(second);
                bool result = q1.Equals(q2);

                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "COMPARE", first.Value, first.Unit, second.Value, second.Unit,
                    result ? 1 : 0, "Boolean", false, null, first.MeasurementType ?? "N/A"));

                return result;
            }
            catch (Exception ex) when (ex is not QuantityMeasurementException)
            {
                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "COMPARE", first.Value, first.Unit, second.Value, second.Unit, ex.Message));
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
                if (!string.Equals(first.MeasurementType, second.MeasurementType, StringComparison.OrdinalIgnoreCase))
                    throw new QuantityMeasurementException("Measurement categories do not match.");

                var q1 = ConvertDtoToModel(first);
                var q2 = ConvertDtoToModel(second);
                double result = q1.Divide(q2);

                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "DIVIDE", first.Value, first.Unit, second.Value, second.Unit, result, "Scalar"));

                return result;
            }
            catch (Exception ex)
            {
                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    "DIVIDE", first.Value, first.Unit, second.Value, second.Unit, ex.Message));
                throw new QuantityMeasurementException($"Division failed: {ex.Message}", ex);
            }
        }

        private QuantityDTO PerformArithmetic(string opName, QuantityDTO first, QuantityDTO second, string targetUnit, QuantityModel<IMeasurable>.ArithmeticOperation operation)
        {
            try
            {
                if (!string.Equals(first.MeasurementType, second.MeasurementType, StringComparison.OrdinalIgnoreCase))
                    throw new QuantityMeasurementException("Measurement categories do not match.");

                var q1 = ConvertDtoToModel(first);
                var q2 = ConvertDtoToModel(second);
                var targetUnitObj = UnitMapper.GetUnitFromString(targetUnit, first.MeasurementType ?? "N/A");

                QuantityModel<IMeasurable> resultQ;
                if (operation == QuantityModel<IMeasurable>.ArithmeticOperation.Add)
                    resultQ = q1.Add(q2, targetUnitObj);
                else
                    resultQ = q1.Subtract(q2, targetUnitObj);

                double resultValue = resultQ.ConvertTo(targetUnitObj);

                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    opName, first.Value, first.Unit, second.Value, second.Unit, resultValue, targetUnit, false, null, first.MeasurementType ?? "N/A"));

                return new QuantityDTO(resultValue, targetUnit, first.MeasurementType ?? "N/A");
            }
            catch (Exception ex) when (ex is not QuantityMeasurementException)
            {
                _repository.SaveMeasurement(new QuantityMeasurementEntity(
                    opName, first.Value, first.Unit, second.Value, second.Unit, ex.Message));
                throw new QuantityMeasurementException($"{opName} failed: {ex.Message}", ex);
            }
        }

        // ==================== REST API Methods (UC17) ====================

        public QuantityMeasurementDTO CompareRest(QuantityInputDTO input)
        {
            try
            {
                var q1 = input.ThisQuantityDTO;
                var q2 = input.ThatQuantityDTO ?? throw new ArgumentException("Second quantity missing");

                if (string.IsNullOrEmpty(q2.MeasurementType))
                    q2.MeasurementType = q1.MeasurementType;

                if (!string.Equals(q1.MeasurementType, q2.MeasurementType, StringComparison.OrdinalIgnoreCase))
                    throw new QuantityMeasurementException($"Cannot compare different measurement categories: {q1.MeasurementType} and {q2.MeasurementType}");

                var model1 = ConvertDtoToModel(q1);
                var model2 = ConvertDtoToModel(q2);
                bool result = model1.Equals(model2);

                var entity = new QuantityMeasurementEntity(
                    "COMPARE", q1.Value, q1.Unit, q2.Value, q2.Unit,
                    result ? 1.0 : 0.0, "Boolean", false, null, q1.MeasurementType ?? "N/A");
                _repository.SaveMeasurement(entity);

                var dto = QuantityMeasurementDTO.FromEntity(entity);
                dto.ResultString = result.ToString().ToLower();
                return dto;
            }
            catch (Exception ex)
            {
                var q1 = input.ThisQuantityDTO;
                var q2 = input.ThatQuantityDTO;
                var entity = new QuantityMeasurementEntity(
                    "COMPARE", q1.Value, q1.Unit, q2?.Value, q2?.Unit,
                    null, null, true, ex.Message, q1.MeasurementType ?? "N/A");
                _repository.SaveMeasurement(entity);
                throw new QuantityMeasurementException(ex.Message, ex);
            }
        }

        public QuantityMeasurementDTO ConvertRest(QuantityInputDTO input)
        {
            try
            {
                var q1 = input.ThisQuantityDTO;
                var targetUnit = input.ThatQuantityDTO?.Unit ?? throw new ArgumentException("Target unit missing");

                var inputModel = ConvertDtoToModel(q1);
                var targetUnitObj = UnitMapper.GetUnitFromString(targetUnit, q1.MeasurementType ?? "N/A");
                double resultValue = inputModel.ConvertTo(targetUnitObj);

                var entity = new QuantityMeasurementEntity(
                    "CONVERT", q1.Value, q1.Unit, null, null,
                    resultValue, targetUnit, false, null, q1.MeasurementType ?? "N/A");
                _repository.SaveMeasurement(entity);

                return QuantityMeasurementDTO.FromEntity(entity);
            }
            catch (Exception ex)
            {
                var q1 = input.ThisQuantityDTO;
                var entity = new QuantityMeasurementEntity(
                    "CONVERT", q1.Value, q1.Unit, null, null,
                    null, null, true, ex.Message, q1.MeasurementType ?? "N/A");
                _repository.SaveMeasurement(entity);
                throw new QuantityMeasurementException($"Conversion failed: {ex.Message}", ex);
            }
        }

        public QuantityMeasurementDTO AddRest(QuantityInputDTO input)
        {
            return PerformArithmeticRest("ADD", input, QuantityModel<IMeasurable>.ArithmeticOperation.Add);
        }

        public QuantityMeasurementDTO SubtractRest(QuantityInputDTO input)
        {
            return PerformArithmeticRest("SUBTRACT", input, QuantityModel<IMeasurable>.ArithmeticOperation.Subtract);
        }

        public QuantityMeasurementDTO DivideRest(QuantityInputDTO input)
        {
            try
            {
                var q1 = input.ThisQuantityDTO;
                var q2 = input.ThatQuantityDTO ?? throw new ArgumentException("Second quantity missing");

                if (string.IsNullOrEmpty(q2.MeasurementType))
                    q2.MeasurementType = q1.MeasurementType;

                if (!string.Equals(q1.MeasurementType, q2.MeasurementType, StringComparison.OrdinalIgnoreCase))
                    throw new QuantityMeasurementException($"Cannot perform arithmetic between different measurement categories: {q1.MeasurementType} and {q2.MeasurementType}");

                var model1 = ConvertDtoToModel(q1);
                var model2 = ConvertDtoToModel(q2);
                double result = model1.Divide(model2);

                var entity = new QuantityMeasurementEntity(
                    "DIVIDE", q1.Value, q1.Unit, q2.Value, q2.Unit,
                    result, "Scalar", false, null, q1.MeasurementType ?? "N/A");
                _repository.SaveMeasurement(entity);

                return QuantityMeasurementDTO.FromEntity(entity);
            }
            catch (Exception ex)
            {
                var q1 = input.ThisQuantityDTO;
                var q2 = input.ThatQuantityDTO;
                var entity = new QuantityMeasurementEntity(
                    "DIVIDE", q1.Value, q1.Unit, q2?.Value, q2?.Unit,
                    null, null, true, ex.Message, q1.MeasurementType ?? "N/A");
                _repository.SaveMeasurement(entity);
                throw new QuantityMeasurementException(ex.Message, ex);
            }
        }

        private QuantityMeasurementDTO PerformArithmeticRest(string opName, QuantityInputDTO input, QuantityModel<IMeasurable>.ArithmeticOperation operation)
        {
            try
            {
                var q1 = input.ThisQuantityDTO;
                var q2 = input.ThatQuantityDTO ?? throw new ArgumentException("Second quantity missing");
                var targetUnit = q1.Unit;

                if (string.IsNullOrEmpty(q2.MeasurementType))
                    q2.MeasurementType = q1.MeasurementType;

                if (!string.Equals(q1.MeasurementType, q2.MeasurementType, StringComparison.OrdinalIgnoreCase))
                    throw new QuantityMeasurementException($"Cannot perform arithmetic between different measurement categories: {q1.MeasurementType} and {q2.MeasurementType}");

                var model1 = ConvertDtoToModel(q1);
                var model2 = ConvertDtoToModel(q2);
                var targetUnitObj = UnitMapper.GetUnitFromString(targetUnit, q1.MeasurementType ?? "N/A");

                QuantityModel<IMeasurable> resultQ;
                if (operation == QuantityModel<IMeasurable>.ArithmeticOperation.Add)
                    resultQ = model1.Add(model2, targetUnitObj);
                else
                    resultQ = model1.Subtract(model2, targetUnitObj);

                double resultValue = resultQ.ConvertTo(targetUnitObj);

                var entity = new QuantityMeasurementEntity(
                    opName, q1.Value, q1.Unit, q2.Value, q2.Unit,
                    resultValue, targetUnit, false, null, q1.MeasurementType ?? "N/A");
                _repository.SaveMeasurement(entity);

                return QuantityMeasurementDTO.FromEntity(entity);
            }
            catch (Exception ex)
            {
                var q1 = input.ThisQuantityDTO;
                var q2 = input.ThatQuantityDTO;
                var entity = new QuantityMeasurementEntity(
                    opName, q1.Value, q1.Unit, q2?.Value, q2?.Unit,
                    null, null, true, ex.Message, q1.MeasurementType ?? "N/A");
                _repository.SaveMeasurement(entity);
                throw new QuantityMeasurementException(ex.Message, ex);
            }
        }

        // ==================== History Methods ====================

        public IEnumerable<QuantityMeasurementDTO> GetOperationHistory(string operation)
        {
            return QuantityMeasurementDTO.FromEntityList(_repository.GetMeasurementsByOperation(operation).ToList());
        }

        public IEnumerable<QuantityMeasurementDTO> GetMeasurementsByType(string type)
        {
            return QuantityMeasurementDTO.FromEntityList(_repository.GetMeasurementsByType(type).ToList());
        }

        public int GetOperationCount(string operation)
        {
            return _repository.GetOperationCount(operation);
        }

        public IEnumerable<QuantityMeasurementDTO> GetErrorHistory()
        {
            return QuantityMeasurementDTO.FromEntityList(_repository.GetErrorMeasurements().ToList());
        }
    }
}
