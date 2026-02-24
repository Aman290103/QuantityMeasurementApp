using System;
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

            QuantityLength resultFeet = QuantityLength.Add(length1, length2, LengthUnit.Feet);
            Console.WriteLine($"Output: Quantity({resultFeet})");

            QuantityLength resultInches = QuantityLength.Add(length1, length2, LengthUnit.Inch);
            Console.WriteLine($"Output: Quantity({resultInches})");

            QuantityLength resultYards = QuantityLength.Add(length1, length2, LengthUnit.Yard);
            Console.WriteLine($"Output: Quantity({resultYards})");

            QuantityLength length3 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength length4 = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength resultYardToYard = QuantityLength.Add(length3, length4, LengthUnit.Yard);
            Console.WriteLine($"Output: Quantity({resultYardToYard})");
        }
    }
}