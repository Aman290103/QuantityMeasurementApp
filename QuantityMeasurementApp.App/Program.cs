﻿using System;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Quantity Measurement App Demo ---\n");

            QuantityLength length1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength length2 = new QuantityLength(12.0, LengthUnit.Inch);

            Console.WriteLine($"Input: Quantity(1.0, FEET).convertTo(INCHES) -> Output: Quantity({length1.ConvertTo(LengthUnit.Inch)}, INCHES)");
            
            QuantityLength resultFeet = length1.Add(length2, LengthUnit.Feet);
            Console.WriteLine($"Input: Quantity(1.0, FEET).add(Quantity(12.0, INCHES), FEET) -> Output: Quantity({resultFeet})");

            QuantityLength length3 = new QuantityLength(36.0, LengthUnit.Inch);
            QuantityLength length4 = new QuantityLength(1.0, LengthUnit.Yard);
            Console.WriteLine($"Input: Quantity(36.0, INCHES).equals(Quantity(1.0, YARDS)) -> Output: {length3.Equals(length4)}");

            QuantityLength length5 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength length6 = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength resultYards = length5.Add(length6, LengthUnit.Yard);
            Console.WriteLine($"Input: Quantity(1.0, YARDS).add(Quantity(3.0, FEET), YARDS) -> Output: Quantity({resultYards})");

            QuantityLength length7 = new QuantityLength(2.54, LengthUnit.Centimeter);
            Console.WriteLine($"Input: Quantity(2.54, CENTIMETERS).convertTo(INCHES) -> Output: Quantity({length7.ConvertTo(LengthUnit.Inch)}, INCHES)");

            QuantityLength length8 = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength length9 = new QuantityLength(0.0, LengthUnit.Inch);
            QuantityLength resultAddZero = length8.Add(length9, LengthUnit.Feet);
            Console.WriteLine($"Input: Quantity(5.0, FEET).add(Quantity(0.0, INCHES), FEET) -> Output: Quantity({resultAddZero})");

            Console.WriteLine($"Input: LengthUnit.FEET.convertToBaseUnit(12.0) -> Output: {LengthUnit.Feet.ConvertToBaseUnit(12.0)}");
            Console.WriteLine($"Input: LengthUnit.INCHES.convertToBaseUnit(12.0) -> Output: {LengthUnit.Inch.ConvertToBaseUnit(12.0)}");
        }
    }
}