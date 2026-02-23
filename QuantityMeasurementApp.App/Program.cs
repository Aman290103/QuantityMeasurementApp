using System;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Quantity Measurement App Demo ---\n");

            demonstrateLengthConversion(1.0, LengthUnit.Feet, LengthUnit.Inch);
            demonstrateLengthConversion(1.0, LengthUnit.Yard, LengthUnit.Feet);
            demonstrateLengthConversion(1.0, LengthUnit.Centimeter, LengthUnit.Inch);

            Console.WriteLine();

            QuantityLength qty = new QuantityLength(36.0, LengthUnit.Inch);
            demonstrateLengthConversion(qty, LengthUnit.Yard);

            Console.WriteLine();

            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength q2 = new QuantityLength(3.0, LengthUnit.Feet);
            demonstrateLengthEquality(q1, q2);

            demonstrateLengthComparison(2.0, LengthUnit.Inch, 5.0, LengthUnit.Centimeter);
        }

        public static void demonstrateLengthConversion(double value, LengthUnit source, LengthUnit target)
        {
            QuantityLength quantity = new QuantityLength(value, source);
            double result = quantity.ConvertTo(target);
            Console.WriteLine($"Converted: {value} {source} -> {result} {target}");
        }

        public static void demonstrateLengthConversion(QuantityLength quantity, LengthUnit target)
        {
            double result = quantity.ConvertTo(target);
            Console.WriteLine($"Converted Object: {quantity} -> {result} {target}");
        }

        public static void demonstrateLengthEquality(QuantityLength q1, QuantityLength q2)
        {
            Console.WriteLine($"Equality Check: {q1} == {q2} ? {q1.Equals(q2)}");
        }

        public static void demonstrateLengthComparison(double v1, LengthUnit u1, double v2, LengthUnit u2)
        {
            QuantityLength q1 = new QuantityLength(v1, u1);
            QuantityLength q2 = new QuantityLength(v2, u2);
            demonstrateLengthEquality(q1, q2);
        }
    }
}