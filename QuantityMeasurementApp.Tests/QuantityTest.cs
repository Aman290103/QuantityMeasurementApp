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

        // --- Volume Enum Setup Tests ---

        [Test]
        public void testIMeasurableInterface_VolumeUnitImplementation()
        {
            IMeasurable unit = VolumeUnit.Litre;
            Assert.AreEqual("Litre", unit.GetUnitName());
            Assert.AreEqual(1.0, unit.GetConversionFactor());
        }

        [Test]
        public void testVolumeUnitEnum_LitreConstant()
        {
            Assert.AreEqual(1.0, VolumeUnit.Litre.GetConversionFactor());
        }

        [Test]
        public void testVolumeUnitEnum_MillilitreConstant()
        {
            Assert.AreEqual(0.001, VolumeUnit.Millilitre.GetConversionFactor());
        }

        [Test]
        public void testVolumeUnitEnum_GallonConstant()
        {
            Assert.AreEqual(3.78541, VolumeUnit.Gallon.GetConversionFactor());
        }

        [Test]
        public void testConvertToBaseUnit_LitreToLitre()
        {
            Assert.AreEqual(5.0, VolumeUnit.Litre.ConvertToBaseUnit(5.0));
        }

        [Test]
        public void testConvertToBaseUnit_MillilitreToLitre()
        {
            Assert.AreEqual(1.0, VolumeUnit.Millilitre.ConvertToBaseUnit(1000.0));
        }

        [Test]
        public void testConvertToBaseUnit_GallonToLitre()
        {
            Assert.AreEqual(3.78541, VolumeUnit.Gallon.ConvertToBaseUnit(1.0));
        }

        [Test]
        public void testConvertFromBaseUnit_LitreToLitre()
        {
            Assert.AreEqual(2.0, VolumeUnit.Litre.ConvertFromBaseUnit(2.0));
        }

        [Test]
        public void testConvertFromBaseUnit_LitreToMillilitre()
        {
            Assert.AreEqual(1000.0, VolumeUnit.Millilitre.ConvertFromBaseUnit(1.0));
        }

        [Test]
        public void testConvertFromBaseUnit_LitreToGallon()
        {
            Assert.AreEqual(1.0, VolumeUnit.Gallon.ConvertFromBaseUnit(3.78541), 0.0001);
        }

        // --- Generic Quantity: Volume Equality Operations ---

        [Test]
        public void testEquality_LitreToLitre_SameValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_LitreToLitre_DifferentValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);
            Assert.IsFalse(first.Equals(second));
        }

        [Test]
        public void testEquality_LitreToMillilitre_EquivalentValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_MillilitreToLitre_EquivalentValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_LitreToGallon_EquivalentValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(0.264172, VolumeUnit.Gallon);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_GallonToLitre_EquivalentValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_VolumeVsLength_Incompatible()
        {
            Quantity<VolumeUnit> volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<LengthUnit> length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.IsFalse(volume.Equals(length));
        }

        [Test]
        public void testEquality_VolumeVsWeight_Incompatible()
        {
            Quantity<VolumeUnit> volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<WeightUnit> weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.IsFalse(volume.Equals(weight));
        }

        [Test]
        public void testEquality_NullComparison()
        {
            Quantity<VolumeUnit> volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsFalse(volume.Equals(null));
        }

        [Test]
        public void testEquality_SameReference()
        {
            Quantity<VolumeUnit> volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsTrue(volume.Equals(volume));
        }

        [Test]
        public void testEquality_NullUnit()
        {
            Assert.Throws<ArgumentNullException>(() => new Quantity<VolumeUnit>(1.0, null!));
        }

        [Test]
        public void testEquality_TransitiveProperty()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> third = new Quantity<VolumeUnit>(0.264172, VolumeUnit.Gallon);

            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(second.Equals(third));
            Assert.IsTrue(first.Equals(third));
        }

        [Test]
        public void testEquality_ZeroValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(0.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_NegativeVolume()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(-1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(-1000.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_LargeVolumeValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_SmallVolumeValue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(0.001, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        // --- Generic Quantity: Volume Conversion Operations ---

        [Test]
        public void testConversion_LitreToMillilitre()
        {
            Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.AreEqual(1000.0, quantity.ConvertTo(VolumeUnit.Millilitre));
        }

        [Test]
        public void testConversion_MillilitreToLitre()
        {
            Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(1.0, quantity.ConvertTo(VolumeUnit.Litre));
        }

        [Test]
        public void testConversion_GallonToLitre()
        {
            Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Assert.AreEqual(3.78541, quantity.ConvertTo(VolumeUnit.Litre), 0.0001);
        }

        [Test]
        public void testConversion_LitreToGallon()
        {
            Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Assert.AreEqual(1.0, quantity.ConvertTo(VolumeUnit.Gallon), 0.0001);
        }

        [Test]
        public void testConversion_MillilitreToGallon()
        {
            Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(0.264172, quantity.ConvertTo(VolumeUnit.Gallon), 0.0001);
        }

        [Test]
        public void testConversion_SameUnit()
        {
            Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Assert.AreEqual(5.0, quantity.ConvertTo(VolumeUnit.Litre));
        }

        [Test]
        public void testConversion_ZeroValue()
        {
            Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre);
            Assert.AreEqual(0.0, quantity.ConvertTo(VolumeUnit.Millilitre));
        }

        [Test]
        public void testConversion_NegativeValue()
        {
            Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(-1.0, VolumeUnit.Litre);
            Assert.AreEqual(-1000.0, quantity.ConvertTo(VolumeUnit.Millilitre));
        }

        [Test]
        public void testConversion_RoundTrip()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.5, VolumeUnit.Litre);
            double ml = first.ConvertTo(VolumeUnit.Millilitre);
            Quantity<VolumeUnit> mid = new Quantity<VolumeUnit>(ml, VolumeUnit.Millilitre);
            Assert.AreEqual(1.5, mid.ConvertTo(VolumeUnit.Litre), 0.0001);
        }

        // --- Generic Quantity: Volume Addition Operations ---

        [Test]
        public void testAddition_SameUnit_LitrePlusLitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_SameUnit_MillilitrePlusMillilitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre)));
        }

        [Test]
        public void testAddition_CrossUnit_LitrePlusMillilitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_CrossUnit_MillilitrePlusLitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(2000.0, VolumeUnit.Millilitre)));
        }

        [Test]
        public void testAddition_CrossUnit_GallonPlusLitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(2.0, VolumeUnit.Gallon)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Litre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = first.Add(second, VolumeUnit.Litre);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Millilitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = first.Add(second, VolumeUnit.Millilitre);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(2000.0, VolumeUnit.Millilitre)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Gallon()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second, VolumeUnit.Gallon);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(2.0, VolumeUnit.Gallon)));
        }

        [Test]
        public void testAddition_Commutativity()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);

            Quantity<VolumeUnit> result1 = first.Add(second, VolumeUnit.Litre);
            Quantity<VolumeUnit> result2 = second.Add(first, VolumeUnit.Litre);

            Assert.IsTrue(result1.Equals(result2));
        }

        [Test]
        public void testAddition_WithZero()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(0.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_NegativeValues()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(-2000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_LargeValues()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(2000000.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_SmallValues()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(0.001, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(0.002, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new Quantity<VolumeUnit>(0.003, VolumeUnit.Litre)));
        }

        [Test]
        public void testGenericQuantity_VolumeOperations_Consistency()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testScalability_VolumeIntegration()
        {
            Quantity<VolumeUnit> volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Assert.AreEqual(3.78541, volume.ConvertTo(VolumeUnit.Litre), 0.0001);
        }

        // --- Generic Quantity: Subtraction Operations ---

        [Test]
        public void testSubtraction_SameUnit_FeetMinusFeet()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<LengthUnit>(5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_SameUnit_LitreMinusLitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<VolumeUnit>(7.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testSubtraction_CrossUnit_FeetMinusInches()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<LengthUnit>(9.5, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_CrossUnit_InchesMinusFeet()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(120.0, LengthUnit.Inch);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<LengthUnit>(60.0, LengthUnit.Inch)));
        }

        [Test]
        public void testSubtraction_ExplicitTargetUnit_Feet()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second, LengthUnit.Feet).Equals(new Quantity<LengthUnit>(9.5, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_ExplicitTargetUnit_Inches()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second, LengthUnit.Inch).Equals(new Quantity<LengthUnit>(114.0, LengthUnit.Inch)));
        }

        [Test]
        public void testSubtraction_ExplicitTargetUnit_Millilitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Subtract(second, VolumeUnit.Millilitre).Equals(new Quantity<VolumeUnit>(3000.0, VolumeUnit.Millilitre)));
        }

        [Test]
        public void testSubtraction_ResultingInNegative()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<LengthUnit>(-5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_ResultingInZero()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(120.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<LengthUnit>(0.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_WithZeroOperand()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(0.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<LengthUnit>(5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_WithNegativeValues()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(-2.0, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<LengthUnit>(7.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_NonCommutative()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsFalse(a.Subtract(b).Equals(b.Subtract(a)));
        }

        [Test]
        public void testSubtraction_WithLargeValues()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(1e6, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(5e5, WeightUnit.Kilogram);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<WeightUnit>(5e5, WeightUnit.Kilogram)));
        }

        [Test]
        public void testSubtraction_WithSmallValues()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(0.001, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(0.0005, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new Quantity<LengthUnit>(0.0005, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_NullOperand()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => first.Subtract(null!));
        }

        [Test]
        public void testSubtraction_NullTargetUnit()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => first.Subtract(second, null!));
        }

        [Test]
        public void testSubtraction_ChainedOperations()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Quantity<LengthUnit> third = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Quantity<LengthUnit> result = first.Subtract(second).Subtract(third);
            Assert.IsTrue(result.Equals(new Quantity<LengthUnit>(7.0, LengthUnit.Feet)));
        }

        // --- Generic Quantity: Division Operations ---

        [Test]
        public void testDivision_SameUnit_FeetDividedByFeet()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(5.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_SameUnit_LitreDividedByLitre()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Assert.AreEqual(2.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_CrossUnit_FeetDividedByInches()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(24.0, LengthUnit.Inch);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(1.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_CrossUnit_KilogramDividedByGram()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(2.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(2000.0, WeightUnit.Gram);
            Assert.AreEqual(1.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_RatioGreaterThanOne()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(5.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_RatioLessThanOne()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.AreEqual(0.5, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_RatioEqualToOne()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.AreEqual(1.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_NonCommutative()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreNotEqual(a.Divide(b), b.Divide(a));
        }

        [Test]
        public void testDivision_ByZero()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> zero = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);
            Assert.Throws<DivideByZeroException>(() => first.Divide(zero));
        }

        [Test]
        public void testDivision_WithLargeRatio()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(1e6, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(1e6, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_WithSmallRatio()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(1e6, WeightUnit.Kilogram);
            Assert.AreEqual(1e-6, first.Divide(second), 1e-8);
        }

        [Test]
        public void testDivision_NullOperand()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => first.Divide(null!));
        }

        [Test]
        public void testSubtractionAndDivision_Integration()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Quantity<LengthUnit> c = new Quantity<LengthUnit>(4.0, LengthUnit.Feet);

            double result = a.Subtract(b).Divide(c); // (10 - 2) / 4 = 2.0
            Assert.AreEqual(2.0, result, 0.0001);
        }

        [Test]
        public void testSubtractionAddition_Inverse()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);

            Quantity<LengthUnit> result = a.Add(b).Subtract(b);
            Assert.IsTrue(a.Equals(result));
        }

        // --- UC13 Centralized DRY Arithmetic Tests ---

        [Test]
        public void testValidation_NullOperand_ConsistentAcrossOperations()
        {
            Quantity<LengthUnit> quantity = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);

            Assert.Throws<ArgumentNullException>(() => quantity.Add(null!));
            Assert.Throws<ArgumentNullException>(() => quantity.Subtract(null!));
            Assert.Throws<ArgumentNullException>(() => quantity.Divide(null!));
        }

        [Test]
        public void testValidation_NullTargetUnit_AddSubtractReject()
        {
            Quantity<LengthUnit> quantity = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> other = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);

            Assert.Throws<ArgumentNullException>(() => quantity.Add(other, null!));
            Assert.Throws<ArgumentNullException>(() => quantity.Subtract(other, null!));
        }

        [Test]
        public void testArithmeticOperation_Add_EnumComputation()
        {
            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Add(q2).Equals(new Quantity<LengthUnit>(15.0, LengthUnit.Feet)));
        }

        [Test]
        public void testArithmeticOperation_Subtract_EnumComputation()
        {
            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Subtract(q2).Equals(new Quantity<LengthUnit>(5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testArithmeticOperation_Divide_EnumComputation()
        {
            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(2.0, q1.Divide(q2), 0.0001);
        }

        [Test]
        public void testArithmeticOperation_DivideByZero_EnumThrows()
        {
            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> zero = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);
            Assert.Throws<DivideByZeroException>(() => q1.Divide(zero));
        }

        [Test]
        public void testRefactoring_NoBehaviorChange_LargeDataset()
        {
            Quantity<VolumeUnit> vol1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> vol2 = new Quantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);

            Assert.IsTrue(vol1.Add(vol2).Equals(new Quantity<VolumeUnit>(5.5, VolumeUnit.Litre)));
            Assert.IsTrue(vol1.Subtract(vol2).Equals(new Quantity<VolumeUnit>(4.5, VolumeUnit.Litre)));
            Assert.AreEqual(10.0, vol1.Divide(vol2), 0.0001);
        }
    }
}