using QuantityMeasurementBusinessLayer.Interface;

namespace QuantityMeasurementBusinessLayer.Service
{
    // VolumeUnitMeasurable provides measurable behavior for VolumeUnit
    public class VolumeUnitMeasurable : IMeasurable
    {
        private readonly VolumeUnit _unit;

        public VolumeUnitMeasurable(VolumeUnit unit)
        {
            _unit = unit;
        }

        public double GetConversionFactor() => _unit.GetConversionFactor();

        public double ConvertToBaseUnit(double value) => _unit.ConvertToBaseUnit(value);


        public double ConvertFromBaseUnit(double baseValue) => _unit.ConvertFromBaseUnit(baseValue);

        public string GetUnitName() => _unit.GetUnitName();
    }
}
