﻿using System;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Quantity Measurement App Demo ---\n");

            // --- Addition ---
            Quantity<LengthUnit> length1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> length2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Console.WriteLine($"Added: Quantity(1.0, FEET).add(Quantity(12.0, INCHES), FEET) -> Output: {length1.Add(length2, LengthUnit.Feet)}\n");

            // --- Subtraction ---
            Console.WriteLine("--- Subtraction Demonstrations ---");
            Quantity<LengthUnit> length3 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> length4 = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Console.WriteLine($"Subtract (Implicit): {length3} - {length4} -> {length3.Subtract(length4)}");
            Console.WriteLine($"Subtract (Explicit): {length3} - {length4} -> {length3.Subtract(length4, LengthUnit.Inch)}");

            Quantity<VolumeUnit> vol1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> vol2 = new Quantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);
            Console.WriteLine($"Subtract Volume: {vol1} - {vol2} -> {vol1.Subtract(vol2)}");

            Quantity<WeightUnit> weight1 = new Quantity<WeightUnit>(2.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> weight2 = new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram);
            Console.WriteLine($"Subtract (Negative Result): {weight1} - {weight2} -> {weight1.Subtract(weight2)}\n");

            // --- Division ---
            Console.WriteLine("--- Division Demonstrations ---");
            Console.WriteLine($"Divide (Same Unit): {new Quantity<LengthUnit>(10.0, LengthUnit.Feet)} / {new Quantity<LengthUnit>(2.0, LengthUnit.Feet)} -> {new Quantity<LengthUnit>(10.0, LengthUnit.Feet).Divide(new Quantity<LengthUnit>(2.0, LengthUnit.Feet))}");
            Console.WriteLine($"Divide (Ratio < 1): {new Quantity<LengthUnit>(5.0, LengthUnit.Feet)} / {new Quantity<LengthUnit>(10.0, LengthUnit.Feet)} -> {new Quantity<LengthUnit>(5.0, LengthUnit.Feet).Divide(new Quantity<LengthUnit>(10.0, LengthUnit.Feet))}");
            Console.WriteLine($"Divide (Cross Unit): {new Quantity<LengthUnit>(24.0, LengthUnit.Inch)} / {new Quantity<LengthUnit>(2.0, LengthUnit.Feet)} -> {new Quantity<LengthUnit>(24.0, LengthUnit.Inch).Divide(new Quantity<LengthUnit>(2.0, LengthUnit.Feet))}");
            Console.WriteLine($"Divide (Weight): {new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram)} / {new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)} -> {new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram).Divide(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram))}");
            Console.WriteLine();
        }
    }
}