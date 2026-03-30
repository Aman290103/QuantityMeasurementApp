using System;
using System.Collections.Generic;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.App
{
    internal static class MenuHelpers
    {
        internal static readonly Dictionary<string, string[]> Units = new Dictionary<string, string[]>
        {
            ["Length"] = new string[] { "Feet", "Inch", "Yard", "Centimeter" },
            ["Volume"] = new string[] { "Litre", "Millilitre", "Gallon" },
            ["Weight"] = new string[] { "Kilogram", "Gram", "Pound" },
            ["Temperature"] = new string[] { "Celsius", "Fahrenheit", "Kelvin" }
        };

        internal static int PickFromMenu(string title, string[] options)
        {
            Console.WriteLine("\n--- " + title + " ---");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + options[i]);
            }

            while (true)
            {
                Console.Write("Enter choice: ");
                string input = Console.ReadLine();
                try
                {
                    int choice = int.Parse(input);
                    if (choice >= 1 && choice <= options.Length)
                    {
                        return choice;
                    }
                    Console.WriteLine("Invalid choice. Enter a valid number from the menu.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        internal static string PickMeasurementType(string[] allowedTypes)
        {
            int choice = PickFromMenu("Select Measurement Type", allowedTypes);
            return allowedTypes[choice - 1];
        }

        internal static string PickUnit(string prompt, string[] units)
        {
            int choice = PickFromMenu(prompt, units);
            return units[choice - 1];
        }

        internal static QuantityDTO ReadQuantity(string label, string measurementType, string[] units)
        {
            string unit = PickUnit(label + " Unit", units);
            double value = ReadDouble("Enter " + label + " Value (" + unit + "): ");
            return new QuantityDTO(value, unit, measurementType);
        }

        internal static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                try
                {
                    double value = double.Parse(input);
                    return value;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid number.");
                }
            }
        }
    }
}
