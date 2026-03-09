﻿using System;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Quantity Measurement App Demo ---\n");

            // ... [Keep existing Length, Weight, Volume demos] ...

            // --- Temperature Operations ---
            Console.WriteLine("--- Temperature Demonstrations ---");
            Quantity<TemperatureUnit> temp1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> temp2 = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.Fahrenheit);
            DemonstrateEquality(temp1, temp2);

            Quantity<TemperatureUnit> tempConverted = new Quantity<TemperatureUnit>(temp1.ConvertTo(TemperatureUnit.Kelvin), TemperatureUnit.Kelvin);
            Console.WriteLine($"Converted: Quantity(100.0, CELSIUS).convertTo(KELVIN) -> Output: {tempConverted}");

            Console.WriteLine("Attempting unsupported addition (100 Celsius + 50 Celsius):");
            try
            {
                Quantity<TemperatureUnit> temp3 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
                temp1.Add(temp3);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Error Caught: {ex.Message}");
            }
            
            Console.WriteLine();
        }

        public static void DemonstrateEquality<T>(Quantity<T> q1, Quantity<T> q2) where T : IMeasurable
        {
            Console.WriteLine($"Equality: {q1} equals {q2} -> Output: {q1.Equals(q2)}");
        }
    }
}