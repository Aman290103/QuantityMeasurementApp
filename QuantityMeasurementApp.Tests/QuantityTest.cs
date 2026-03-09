using System;
using NUnit.Framework;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityTests
    {
        // --- Interface & Enum Setup Tests ---

        [Test]
        public void testIMeasurableInterface_LengthUnitImplementation()
        {
            IMeasurable unit = LengthUnit.Feet;
            Assert.AreEqual("Feet", unit.GetUnitName());
            Assert.AreEqual(1.0, unit.GetConversionFactor());
        }

        [Test]
        public void testIMeasurableInterface_WeightUnitImplementation()
        {
            IMeasurable unit = WeightUnit.Kilogram;
            Assert.AreEqual("Kilogram", unit.GetUnitName());
            Assert.AreEqual(1.0, unit.GetConversionFactor());
        }

        // --- Generic Quantity: Length Operations ---

        [Test]
        public void testGenericQuantity_LengthOperations_Equality()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testGenericQuantity_LengthOperations_Conversion()
        {
            Quantity<LengthUnit> quantity = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.AreEqual(12.0, quantity.ConvertTo(LengthUnit.Inch));
        }

        [Test]
        public void testGenericQuantity_LengthOperations_Addition()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = first.Add(second, LengthUnit.Feet);
            
            Assert.IsTrue(result.Equals(new Quantity<LengthUnit>(2.0, LengthUnit.Feet)));
        }

        // --- Generic Quantity: Weight Operations ---

        [Test]
        public void testGenericQuantity_WeightOperations_Equality()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testGenericQuantity_WeightOperations_Conversion()
        {
            Quantity<WeightUnit> quantity = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(1000.0, quantity.ConvertTo(WeightUnit.Gram));
        }

        [Test]
        public void testGenericQuantity_WeightOperations_Addition()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            Quantity<WeightUnit> result = first.Add(second, WeightUnit.Kilogram);
            
            Assert.IsTrue(result.Equals(new Quantity<WeightUnit>(2.0, WeightUnit.Kilogram)));
        }

        // --- Cross-Category & Validation ---

        [Test]
        public void testCrossCategoryPrevention_LengthVsWeight()
        {
            Quantity<LengthUnit> length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            
            // In C#, object.Equals(object) accepts any object. Generics make them different types at runtime.
            Assert.IsFalse(length.Equals(new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram)));
        }

        [Test]
        public void testGenericQuantity_ConstructorValidation_NullUnit()
        {
            Assert.Throws<ArgumentNullException>(() => new Quantity<LengthUnit>(1.0, null!));
        }

        [Test]
        public void testGenericQuantity_ConstructorValidation_InvalidValue()
        {
            Assert.Throws<ArgumentException>(() => new Quantity<LengthUnit>(double.NaN, LengthUnit.Feet));
        }
    }
}