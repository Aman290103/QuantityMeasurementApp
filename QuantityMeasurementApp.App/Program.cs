﻿using System;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Quantity Measurement App Demo ---\n");

            // Length Operations
            Quantity<LengthUnit> length1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> length2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            DemonstrateEquality(length1, length2);

            Quantity<LengthUnit> lengthConverted = new Quantity<LengthUnit>(length1.ConvertTo(LengthUnit.Inch), LengthUnit.Inch);
            Console.WriteLine($"Converted: Quantity(1.0, FEET).convertTo(INCHES) -> Output: {lengthConverted}");

            Quantity<LengthUnit> lengthAdded = length1.Add(length2, LengthUnit.Feet);
            Console.WriteLine($"Added: Quantity(1.0, FEET).add(Quantity(12.0, INCHES), FEET) -> Output: {lengthAdded}\n");

            // Weight Operations
            Quantity<WeightUnit> weight1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> weight2 = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            DemonstrateEquality(weight1, weight2);

            Quantity<WeightUnit> weightConverted = new Quantity<WeightUnit>(weight1.ConvertTo(WeightUnit.Gram), WeightUnit.Gram);
            Console.WriteLine($"Converted: Quantity(1.0, KILOGRAM).convertTo(GRAM) -> Output: {weightConverted}");

            Quantity<WeightUnit> weightAdded = weight1.Add(weight2, WeightUnit.Kilogram);
            Console.WriteLine($"Added: Quantity(1.0, KILOGRAM).add(Quantity(1000.0, GRAM), KILOGRAM) -> Output: {weightAdded}\n");

            // Cross Category compile-time safety check (uncommenting the line below causes a compiler error)
            // Console.WriteLine(length1.Equals(weight1));
        }

        // Generic Demonstration Method
        public static void DemonstrateEquality<T>(Quantity<T> q1, Quantity<T> q2) where T : IMeasurable
        {
            Console.WriteLine($"Equality: {q1} equals {q2} -> Output: {q1.Equals(q2)}");
        }
    }
}