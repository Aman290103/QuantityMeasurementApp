using System;
using System.Linq;
using System.Reflection;

namespace QuantityMeasurementApp.Entity
{
    // Utility to map string unit names to IMeasurable objects
    public static class UnitMapper
    {
        public static IMeasurable GetUnitFromString(string unitName, string type)
        {
            if (string.IsNullOrWhiteSpace(unitName) || string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Unit name or type cannot be empty.");

            return type.ToUpper() switch
            {
                "LENGTH" => GetFromStaticFields<LengthUnit>(unitName),
                "VOLUME" => GetFromStaticFields<VolumeUnit>(unitName),
                "WEIGHT" => GetFromStaticFields<WeightUnit>(unitName),
                "TEMPERATURE" => GetFromStaticFields<Temperature>(unitName),
                _ => throw new ArgumentException($"Invalid Measurement Type: {type}")
            };
        }

        private static IMeasurable GetFromStaticFields<T>(string unitName) where T : IMeasurable
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => typeof(IMeasurable).IsAssignableFrom(f.FieldType));
                
            foreach (var field in fields)
            {
                if (field.Name.Equals(unitName, StringComparison.OrdinalIgnoreCase))
                {
                    return (IMeasurable)field.GetValue(null)!;
                }
            }
            throw new ArgumentException($"Invalid Unit '{unitName}' for {typeof(T).Name}");
        }
    }
}
