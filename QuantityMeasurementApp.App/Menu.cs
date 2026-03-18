using System;
using System.Collections.Generic;
using System.Linq;
using QuantityMeasurementApp.Controllers;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.App
{
    public class Menu : IMenu
    {
        // ─── Unit reference tables ────────────────────────────────────────────
        private static readonly Dictionary<string, string[]> Units = new()
        {
            ["Length"]      = new[] { "Feet", "Inch", "Yard", "Centimeter" },
            ["Volume"]      = new[] { "Litre", "Millilitre", "Gallon" },
            ["Weight"]      = new[] { "Kilogram", "Gram", "Pound" },
            ["Temperature"] = new[] { "Celsius", "Fahrenheit", "Kelvin" }
        };

        private readonly QuantityMeasurementController _controller;

        public Menu(QuantityMeasurementController controller)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        // ─── IMenu ────────────────────────────────────────────────────────────

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                PrintHeader();
                int opChoice = PickFromMenu("Select Operation",
                    "1. Compare two quantities",
                    "2. Convert a quantity",
                    "3. Add two quantities",
                    "4. Subtract two quantities",
                    "5. Divide two quantities",
                    "6. Exit");

                if (opChoice == 6) { Bye(); return; }

                string   measurementType = PickMeasurementType();
                string[] available       = Units[measurementType];

                Console.WriteLine();
                switch (opChoice)
                {
                    // ── 1. COMPARE ───────────────────────────────────────────
                    case 1:
                    {
                        var q1 = ReadQuantity("First  quantity", measurementType, available);
                        var q2 = ReadQuantity("Second quantity", measurementType, available);
                        Console.WriteLine();
                        _controller.PerformComparison(q1, q2);
                        break;
                    }

                    // ── 2. CONVERT ───────────────────────────────────────────
                    case 2:
                    {
                        var    src    = ReadQuantity("Source quantity", measurementType, available);
                        string target = PickUnit("Convert TO", available);
                        Console.WriteLine();
                        _controller.PerformConversion(src, target);
                        break;
                    }

                    // ── 3. ADD ────────────────────────────────────────────────
                    case 3:
                    {
                        var    q1     = ReadQuantity("First  quantity", measurementType, available);
                        var    q2     = ReadQuantity("Second quantity", measurementType, available);
                        string target = PickUnit("Result unit", available);
                        Console.WriteLine();
                        _controller.PerformAddition(q1, q2, target);
                        break;
                    }

                    // ── 4. SUBTRACT ───────────────────────────────────────────
                    case 4:
                    {
                        var    q1     = ReadQuantity("First  quantity", measurementType, available);
                        var    q2     = ReadQuantity("Second quantity", measurementType, available);
                        string target = PickUnit("Result unit", available);
                        Console.WriteLine();
                        _controller.PerformSubtraction(q1, q2, target);
                        break;
                    }

                    // ── 5. DIVIDE ─────────────────────────────────────────────
                    case 5:
                    {
                        var q1 = ReadQuantity("Numerator   quantity", measurementType, available);
                        var q2 = ReadQuantity("Denominator quantity", measurementType, available);
                        Console.WriteLine();
                        _controller.PerformDivision(q1, q2);
                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nPress any key to return to main menu...");
                Console.ResetColor();
                Console.ReadKey(true);
            }
        }

        // ─── Private helpers ──────────────────────────────────────────────────

        private static void PrintHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║      Quantity Measurement App  v1.0          ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static int PickFromMenu(string title, params string[] options)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  {title}");
            Console.ResetColor();
            Console.WriteLine("  " + new string('─', 40));

            foreach (var option in options)
                Console.WriteLine($"  {option}");

            Console.WriteLine();
            while (true)
            {
                Console.Write("  Enter choice: ");
                if (int.TryParse(Console.ReadLine()?.Trim(), out int choice)
                    && choice >= 1 && choice <= options.Length)
                    return choice;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ Please enter a number between 1 and {options.Length}.");
                Console.ResetColor();
            }
        }

        private static string PickMeasurementType()
        {
            Console.WriteLine();
            string[] types     = Units.Keys.ToArray();
            string[] menuItems = types.Select((t, i) => $"{i + 1}. {t}").ToArray();
            int      choice    = PickFromMenu("Select Measurement Type", menuItems);
            return types[choice - 1];
        }

        private static string PickUnit(string prompt, string[] units)
        {
            Console.WriteLine();
            string[] menuItems = units.Select((u, i) => $"{i + 1}. {u}").ToArray();
            int      choice    = PickFromMenu(prompt, menuItems);
            return units[choice - 1];
        }

        private static QuantityDTO ReadQuantity(string label, string measurementType, string[] units)
        {
            string unit  = PickUnit(label + " – pick unit", units);
            double value = ReadDouble($"  Enter value in {unit}: ");
            return new QuantityDTO(value, unit, measurementType);
        }

        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine()?.Trim(),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double value))
                    return value;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✗ Invalid number. Please try again.");
                Console.ResetColor();
            }
        }

        private static void Bye()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n  👋  Thanks for using Quantity Measurement App. Goodbye!\n");
            Console.ResetColor();
        }
    }
}
