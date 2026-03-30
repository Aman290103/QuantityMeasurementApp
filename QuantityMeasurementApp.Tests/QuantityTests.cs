using System;
using NUnit.Framework;
using QuantityMeasurementApp.Entity;

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
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testGenericQuantity_LengthOperations_Conversion()
        {
            QuantityModel<LengthUnit> quantity = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.AreEqual(12.0, quantity.ConvertTo(LengthUnit.Inch));
        }

        [Test]
        public void testGenericQuantity_LengthOperations_Addition()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(12.0, LengthUnit.Inch);
            QuantityModel<LengthUnit> result = first.Add(second, LengthUnit.Feet);

            Assert.IsTrue(result.Equals(new QuantityModel<LengthUnit>(2.0, LengthUnit.Feet)));
        }

        // --- Generic Quantity: Weight Operations ---

        [Test]
        public void testGenericQuantity_WeightOperations_Equality()
        {
            QuantityModel<WeightUnit> first = new QuantityModel<WeightUnit>(1.0, WeightUnit.Kilogram);
            QuantityModel<WeightUnit> second = new QuantityModel<WeightUnit>(1000.0, WeightUnit.Gram);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testGenericQuantity_WeightOperations_Conversion()
        {
            QuantityModel<WeightUnit> quantity = new QuantityModel<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(1000.0, quantity.ConvertTo(WeightUnit.Gram));
        }

        [Test]
        public void testGenericQuantity_WeightOperations_Addition()
        {
            QuantityModel<WeightUnit> first = new QuantityModel<WeightUnit>(1.0, WeightUnit.Kilogram);
            QuantityModel<WeightUnit> second = new QuantityModel<WeightUnit>(1000.0, WeightUnit.Gram);
            QuantityModel<WeightUnit> result = first.Add(second, WeightUnit.Kilogram);

            Assert.IsTrue(result.Equals(new QuantityModel<WeightUnit>(2.0, WeightUnit.Kilogram)));
        }

        // --- Cross-Category & Validation ---

        [Test]
        public void testCrossCategoryPrevention_LengthVsWeight()
        {
            QuantityModel<LengthUnit> length = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);

            // In C#, object.Equals(object) accepts any object. Generics make them different types at runtime.
            Assert.IsFalse(length.Equals(new QuantityModel<WeightUnit>(1.0, WeightUnit.Kilogram)));
        }

        [Test]
        public void testGenericQuantity_ConstructorValidation_NullUnit()
        {
            Assert.Throws<ArgumentNullException>(() => new QuantityModel<LengthUnit>(1.0, null!));
        }

        [Test]
        public void testGenericQuantity_ConstructorValidation_InvalidValue()
        {
            Assert.Throws<ArgumentException>(() => new QuantityModel<LengthUnit>(double.NaN, LengthUnit.Feet));
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
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_LitreToLitre_DifferentValue()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(2.0, VolumeUnit.Litre);
            Assert.IsFalse(first.Equals(second));
        }

        [Test]
        public void testEquality_LitreToMillilitre_EquivalentValue()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_MillilitreToLitre_EquivalentValue()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_LitreToGallon_EquivalentValue()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(0.264172, VolumeUnit.Gallon);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_GallonToLitre_EquivalentValue()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Gallon);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_VolumeVsLength_Incompatible()
        {
            QuantityModel<VolumeUnit> volume = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<LengthUnit> length = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.IsFalse(volume.Equals(length));
        }

        [Test]
        public void testEquality_VolumeVsWeight_Incompatible()
        {
            QuantityModel<VolumeUnit> volume = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<WeightUnit> weight = new QuantityModel<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.IsFalse(volume.Equals(weight));
        }

        [Test]
        public void testEquality_NullComparison()
        {
            QuantityModel<VolumeUnit> volume = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsFalse(volume.Equals(null));
        }

        [Test]
        public void testEquality_SameReference()
        {
            QuantityModel<VolumeUnit> volume = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsTrue(volume.Equals(volume));
        }

        [Test]
        public void testEquality_NullUnit()
        {
            Assert.Throws<ArgumentNullException>(() => new QuantityModel<VolumeUnit>(1.0, null!));
        }

        [Test]
        public void testEquality_TransitiveProperty()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> third = new QuantityModel<VolumeUnit>(0.264172, VolumeUnit.Gallon);

            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(second.Equals(third));
            Assert.IsTrue(first.Equals(third));
        }

        [Test]
        public void testEquality_ZeroValue()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(0.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(0.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_NegativeVolume()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(-1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(-1000.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_LargeVolumeValue()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1000000.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_SmallVolumeValue()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(0.001, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        // --- Generic Quantity: Volume Conversion Operations ---

        [Test]
        public void testConversion_LitreToMillilitre()
        {
            QuantityModel<VolumeUnit> quantity = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.AreEqual(1000.0, quantity.ConvertTo(VolumeUnit.Millilitre));
        }

        [Test]
        public void testConversion_MillilitreToLitre()
        {
            QuantityModel<VolumeUnit> quantity = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(1.0, quantity.ConvertTo(VolumeUnit.Litre));
        }

        [Test]
        public void testConversion_GallonToLitre()
        {
            QuantityModel<VolumeUnit> quantity = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Assert.AreEqual(3.78541, quantity.ConvertTo(VolumeUnit.Litre), 0.0001);
        }

        [Test]
        public void testConversion_LitreToGallon()
        {
            QuantityModel<VolumeUnit> quantity = new QuantityModel<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Assert.AreEqual(1.0, quantity.ConvertTo(VolumeUnit.Gallon), 0.0001);
        }

        [Test]
        public void testConversion_MillilitreToGallon()
        {
            QuantityModel<VolumeUnit> quantity = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(0.264172, quantity.ConvertTo(VolumeUnit.Gallon), 0.0001);
        }

        [Test]
        public void testConversion_SameUnit()
        {
            QuantityModel<VolumeUnit> quantity = new QuantityModel<VolumeUnit>(5.0, VolumeUnit.Litre);
            Assert.AreEqual(5.0, quantity.ConvertTo(VolumeUnit.Litre));
        }

        [Test]
        public void testConversion_ZeroValue()
        {
            QuantityModel<VolumeUnit> quantity = new QuantityModel<VolumeUnit>(0.0, VolumeUnit.Litre);
            Assert.AreEqual(0.0, quantity.ConvertTo(VolumeUnit.Millilitre));
        }

        [Test]
        public void testConversion_NegativeValue()
        {
            QuantityModel<VolumeUnit> quantity = new QuantityModel<VolumeUnit>(-1.0, VolumeUnit.Litre);
            Assert.AreEqual(-1000.0, quantity.ConvertTo(VolumeUnit.Millilitre));
        }

        [Test]
        public void testConversion_RoundTrip()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.5, VolumeUnit.Litre);
            double ml = first.ConvertTo(VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> mid = new QuantityModel<VolumeUnit>(ml, VolumeUnit.Millilitre);
            Assert.AreEqual(1.5, mid.ConvertTo(VolumeUnit.Litre), 0.0001);
        }

        // --- Generic Quantity: Volume Addition Operations ---

        [Test]
        public void testAddition_SameUnit_LitrePlusLitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(2.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(3.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_SameUnit_MillilitrePlusMillilitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(500.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(500.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre)));
        }

        [Test]
        public void testAddition_CrossUnit_LitrePlusMillilitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(2.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_CrossUnit_MillilitrePlusLitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(2000.0, VolumeUnit.Millilitre)));
        }

        [Test]
        public void testAddition_CrossUnit_GallonPlusLitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Gallon);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(3.78541, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(2.0, VolumeUnit.Gallon)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Litre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> result = first.Add(second, VolumeUnit.Litre);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(2.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Millilitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> result = first.Add(second, VolumeUnit.Millilitre);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(2000.0, VolumeUnit.Millilitre)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Gallon()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(3.78541, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(3.78541, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> result = first.Add(second, VolumeUnit.Gallon);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(2.0, VolumeUnit.Gallon)));
        }

        [Test]
        public void testAddition_Commutativity()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);

            QuantityModel<VolumeUnit> result1 = first.Add(second, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> result2 = second.Add(first, VolumeUnit.Litre);

            Assert.IsTrue(result1.Equals(result2));
        }

        [Test]
        public void testAddition_WithZero()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(5.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(0.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(5.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_NegativeValues()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(5.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(-2000.0, VolumeUnit.Millilitre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(3.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_LargeValues()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1000000.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000000.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(2000000.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testAddition_SmallValues()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(0.001, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(0.002, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityModel<VolumeUnit>(0.003, VolumeUnit.Litre)));
        }

        [Test]
        public void testGenericQuantity_VolumeOperations_Consistency()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testScalability_VolumeIntegration()
        {
            QuantityModel<VolumeUnit> volume = new QuantityModel<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Assert.AreEqual(3.78541, volume.ConvertTo(VolumeUnit.Litre), 0.0001);
        }

        // --- Generic Quantity: Subtraction Operations ---

        [Test]
        public void testSubtraction_SameUnit_FeetMinusFeet()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_SameUnit_LitreMinusLitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(10.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(3.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<VolumeUnit>(7.0, VolumeUnit.Litre)));
        }

        [Test]
        public void testSubtraction_CrossUnit_FeetMinusInches()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(6.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<LengthUnit>(9.5, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_CrossUnit_InchesMinusFeet()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(120.0, LengthUnit.Inch);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<LengthUnit>(60.0, LengthUnit.Inch)));
        }

        [Test]
        public void testSubtraction_ExplicitTargetUnit_Feet()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(6.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second, LengthUnit.Feet).Equals(new QuantityModel<LengthUnit>(9.5, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_ExplicitTargetUnit_Inches()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(6.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second, LengthUnit.Inch).Equals(new QuantityModel<LengthUnit>(114.0, LengthUnit.Inch)));
        }

        [Test]
        public void testSubtraction_ExplicitTargetUnit_Millilitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(5.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(2.0, VolumeUnit.Litre);
            Assert.IsTrue(first.Subtract(second, VolumeUnit.Millilitre).Equals(new QuantityModel<VolumeUnit>(3000.0, VolumeUnit.Millilitre)));
        }

        [Test]
        public void testSubtraction_ResultingInNegative()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<LengthUnit>(-5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_ResultingInZero()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(120.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<LengthUnit>(0.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_WithZeroOperand()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(0.0, LengthUnit.Inch);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_WithNegativeValues()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(-2.0, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<LengthUnit>(7.0, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_NonCommutative()
        {
            QuantityModel<LengthUnit> a = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> b = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsFalse(a.Subtract(b).Equals(b.Subtract(a)));
        }

        [Test]
        public void testSubtraction_WithLargeValues()
        {
            QuantityModel<WeightUnit> first = new QuantityModel<WeightUnit>(1e6, WeightUnit.Kilogram);
            QuantityModel<WeightUnit> second = new QuantityModel<WeightUnit>(5e5, WeightUnit.Kilogram);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<WeightUnit>(5e5, WeightUnit.Kilogram)));
        }

        [Test]
        public void testSubtraction_WithSmallValues()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(0.001, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(0.0005, LengthUnit.Feet);
            Assert.IsTrue(first.Subtract(second).Equals(new QuantityModel<LengthUnit>(0.0005, LengthUnit.Feet)));
        }

        [Test]
        public void testSubtraction_NullOperand()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => first.Subtract(null!));
        }

        [Test]
        public void testSubtraction_NullTargetUnit()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => first.Subtract(second, null!));
        }

        [Test]
        public void testSubtraction_ChainedOperations()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(2.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> third = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);

            QuantityModel<LengthUnit> result = first.Subtract(second).Subtract(third);
            Assert.IsTrue(result.Equals(new QuantityModel<LengthUnit>(7.0, LengthUnit.Feet)));
        }

        // --- Generic Quantity: Division Operations ---

        [Test]
        public void testDivision_SameUnit_FeetDividedByFeet()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(5.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_SameUnit_LitreDividedByLitre()
        {
            QuantityModel<VolumeUnit> first = new QuantityModel<VolumeUnit>(10.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> second = new QuantityModel<VolumeUnit>(5.0, VolumeUnit.Litre);
            Assert.AreEqual(2.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_CrossUnit_FeetDividedByInches()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(24.0, LengthUnit.Inch);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(1.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_CrossUnit_KilogramDividedByGram()
        {
            QuantityModel<WeightUnit> first = new QuantityModel<WeightUnit>(2.0, WeightUnit.Kilogram);
            QuantityModel<WeightUnit> second = new QuantityModel<WeightUnit>(2000.0, WeightUnit.Gram);
            Assert.AreEqual(1.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_RatioGreaterThanOne()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(5.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_RatioLessThanOne()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.AreEqual(0.5, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_RatioEqualToOne()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> second = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.AreEqual(1.0, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_NonCommutative()
        {
            QuantityModel<LengthUnit> a = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> b = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreNotEqual(a.Divide(b), b.Divide(a));
        }

        [Test]
        public void testDivision_ByZero()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> zero = new QuantityModel<LengthUnit>(0.0, LengthUnit.Feet);
            Assert.Throws<DivideByZeroException>(() => first.Divide(zero));
        }

        [Test]
        public void testDivision_WithLargeRatio()
        {
            QuantityModel<WeightUnit> first = new QuantityModel<WeightUnit>(1e6, WeightUnit.Kilogram);
            QuantityModel<WeightUnit> second = new QuantityModel<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(1e6, first.Divide(second), 0.0001);
        }

        [Test]
        public void testDivision_WithSmallRatio()
        {
            QuantityModel<WeightUnit> first = new QuantityModel<WeightUnit>(1.0, WeightUnit.Kilogram);
            QuantityModel<WeightUnit> second = new QuantityModel<WeightUnit>(1e6, WeightUnit.Kilogram);
            Assert.AreEqual(1e-6, first.Divide(second), 1e-8);
        }

        [Test]
        public void testDivision_NullOperand()
        {
            QuantityModel<LengthUnit> first = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => first.Divide(null!));
        }

        [Test]
        public void testSubtractionAndDivision_Integration()
        {
            QuantityModel<LengthUnit> a = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> b = new QuantityModel<LengthUnit>(2.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> c = new QuantityModel<LengthUnit>(4.0, LengthUnit.Feet);

            double result = a.Subtract(b).Divide(c); // (10 - 2) / 4 = 2.0
            Assert.AreEqual(2.0, result, 0.0001);
        }

        [Test]
        public void testSubtractionAddition_Inverse()
        {
            QuantityModel<LengthUnit> a = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> b = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);

            QuantityModel<LengthUnit> result = a.Add(b).Subtract(b);
            Assert.IsTrue(a.Equals(result));
        }

        // --- UC13 Centralized DRY Arithmetic Tests ---

        [Test]
        public void testValidation_NullOperand_ConsistentAcrossOperations()
        {
            QuantityModel<LengthUnit> quantity = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);

            Assert.Throws<ArgumentNullException>(() => quantity.Add(null!));
            Assert.Throws<ArgumentNullException>(() => quantity.Subtract(null!));
            Assert.Throws<ArgumentNullException>(() => quantity.Divide(null!));
        }

        [Test]
        public void testValidation_NullTargetUnit_AddSubtractReject()
        {
            QuantityModel<LengthUnit> quantity = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> other = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);

            Assert.Throws<ArgumentNullException>(() => quantity.Add(other, null!));
            Assert.Throws<ArgumentNullException>(() => quantity.Subtract(other, null!));
        }

        [Test]
        public void testArithmeticOperation_Add_EnumComputation()
        {
            QuantityModel<LengthUnit> q1 = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> q2 = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Add(q2).Equals(new QuantityModel<LengthUnit>(15.0, LengthUnit.Feet)));
        }

        [Test]
        public void testArithmeticOperation_Subtract_EnumComputation()
        {
            QuantityModel<LengthUnit> q1 = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> q2 = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Subtract(q2).Equals(new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testArithmeticOperation_Divide_EnumComputation()
        {
            QuantityModel<LengthUnit> q1 = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> q2 = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(2.0, q1.Divide(q2), 0.0001);
        }

        [Test]
        public void testArithmeticOperation_DivideByZero_EnumThrows()
        {
            QuantityModel<LengthUnit> q1 = new QuantityModel<LengthUnit>(10.0, LengthUnit.Feet);
            QuantityModel<LengthUnit> zero = new QuantityModel<LengthUnit>(0.0, LengthUnit.Feet);
            Assert.Throws<DivideByZeroException>(() => q1.Divide(zero));
        }

        [Test]
        public void testRefactoring_NoBehaviorChange_LargeDataset()
        {
            QuantityModel<VolumeUnit> vol1 = new QuantityModel<VolumeUnit>(5.0, VolumeUnit.Litre);
            QuantityModel<VolumeUnit> vol2 = new QuantityModel<VolumeUnit>(500.0, VolumeUnit.Millilitre);

            Assert.IsTrue(vol1.Add(vol2).Equals(new QuantityModel<VolumeUnit>(5.5, VolumeUnit.Litre)));
            Assert.IsTrue(vol1.Subtract(vol2).Equals(new QuantityModel<VolumeUnit>(4.5, VolumeUnit.Litre)));
            Assert.AreEqual(10.0, vol1.Divide(vol2), 0.0001);
        }


        // --- UC14 Temperature Category & Operational Constraints Tests ---

        [Test]
        public void testTemperatureEquality_CelsiusToCelsius_SameValue()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(0.0, Temperature.Celsius);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(0.0, Temperature.Celsius);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testTemperatureEquality_FahrenheitToFahrenheit_SameValue()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(32.0, Temperature.Fahrenheit);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(32.0, Temperature.Fahrenheit);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testTemperatureEquality_CelsiusToFahrenheit_0Celsius32Fahrenheit()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(0.0, Temperature.Celsius);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(32.0, Temperature.Fahrenheit);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testTemperatureEquality_CelsiusToFahrenheit_100Celsius212Fahrenheit()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(100.0, Temperature.Celsius);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(212.0, Temperature.Fahrenheit);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testTemperatureEquality_CelsiusToFahrenheit_Negative40Equal()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(-40.0, Temperature.Celsius);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(-40.0, Temperature.Fahrenheit);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testTemperatureEquality_CelsiusToKelvin_0Celsius273Kelvin()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(0.0, Temperature.Celsius);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(273.15, Temperature.Kelvin);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testTemperatureConversion_CelsiusToFahrenheit_VariousValues()
        {
            QuantityModel<Temperature> q1 = new QuantityModel<Temperature>(50.0, Temperature.Celsius);
            Assert.AreEqual(122.0, q1.ConvertTo(Temperature.Fahrenheit), 0.0001);

            QuantityModel<Temperature> q2 = new QuantityModel<Temperature>(-20.0, Temperature.Celsius);
            Assert.AreEqual(-4.0, q2.ConvertTo(Temperature.Fahrenheit), 0.0001);
        }

        [Test]
        public void testTemperatureConversion_FahrenheitToCelsius_VariousValues()
        {
            QuantityModel<Temperature> q1 = new QuantityModel<Temperature>(122.0, Temperature.Fahrenheit);
            Assert.AreEqual(50.0, q1.ConvertTo(Temperature.Celsius), 0.0001);
        }

        [Test]
        public void testTemperatureConversion_RoundTrip_PreservesValue()
        {
            QuantityModel<Temperature> start = new QuantityModel<Temperature>(37.5, Temperature.Celsius);
            double fahrenheit = start.ConvertTo(Temperature.Fahrenheit);
            QuantityModel<Temperature> mid = new QuantityModel<Temperature>(fahrenheit, Temperature.Fahrenheit);
            
            Assert.AreEqual(37.5, mid.ConvertTo(Temperature.Celsius), 0.0001);
        }

        [Test]
        public void testTemperatureUnsupportedOperation_Add()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(100.0, Temperature.Celsius);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(50.0, Temperature.Celsius);
            
            var ex = Assert.Throws<NotSupportedException>(() => first.Add(second));
            Assert.IsTrue(ex!.Message.Contains("does not support Add operations"));
        }

        [Test]
        public void testTemperatureUnsupportedOperation_Subtract()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(100.0, Temperature.Celsius);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(50.0, Temperature.Celsius);
            
            Assert.Throws<NotSupportedException>(() => first.Subtract(second));
        }

        [Test]
        public void testTemperatureUnsupportedOperation_Divide()
        {
            QuantityModel<Temperature> first = new QuantityModel<Temperature>(100.0, Temperature.Celsius);
            QuantityModel<Temperature> second = new QuantityModel<Temperature>(50.0, Temperature.Celsius);
            
            Assert.Throws<NotSupportedException>(() => first.Divide(second));
        }

        [Test]
        public void testTemperatureVsLengthIncompatibility()
        {
            QuantityModel<Temperature> temp = new QuantityModel<Temperature>(100.0, Temperature.Celsius);
            QuantityModel<LengthUnit> length = new QuantityModel<LengthUnit>(100.0, LengthUnit.Feet);
            Assert.IsFalse(temp.Equals(length));
        }

        [Test]
        public void testOperationSupportMethods_TemperatureAddition()
        {
            Assert.IsFalse(Temperature.Celsius.SupportsArithmetic());
        }

        
    }
}