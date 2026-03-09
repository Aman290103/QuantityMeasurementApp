﻿using System;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Quantity Measurement App Demo ---\n");

            QuantityWeight weight1 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight weight2 = new QuantityWeight(1000.0, WeightUnit.Gram);
            Console.WriteLine($"Equality: Quantity(1.0, KILOGRAM).equals(Quantity(1000.0, GRAM)) -> Output: {weight1.Equals(weight2)}");

            QuantityWeight weight3 = new QuantityWeight(2.0, WeightUnit.Pound);
            Console.WriteLine($"Conversion: Quantity(2.0, POUND).convertTo(KILOGRAM) -> Output: Quantity({weight3.ConvertTo(WeightUnit.Kilogram)}, KILOGRAM)");

            QuantityWeight weight4 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight weight5 = new QuantityWeight(2.0, WeightUnit.Kilogram);
            QuantityWeight resultAdd1 = weight4.Add(weight5);
            Console.WriteLine($"Implicit Add: Quantity(1.0, KILOGRAM).add(Quantity(2.0, KILOGRAM)) -> Output: Quantity({resultAdd1})");

            QuantityWeight weight6 = new QuantityWeight(1.0, WeightUnit.Pound);
            QuantityWeight weight7 = new QuantityWeight(453.592, WeightUnit.Gram);
            QuantityWeight resultAdd2 = weight6.Add(weight7, WeightUnit.Pound);
            Console.WriteLine($"Explicit Add: Quantity(1.0, POUND).add(Quantity(453.592, GRAM), POUND) -> Output: Quantity({resultAdd2})");

            QuantityLength length1 = new QuantityLength(1.0, LengthUnit.Feet);
            Console.WriteLine($"Category Incompatibility: Quantity(1.0, KILOGRAM).equals(Quantity(1.0, FEET)) -> Output: {weight1.Equals(length1)}");
        }
    }
}