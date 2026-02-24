using NUnit.Framework;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityLengthTests
    {
        [Test]
        public void testEquality_FeetToFeet_SameValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_InchToInch_SameValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Inch);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_FeetToInch_EquivalentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_InchToFeet_EquivalentValue()
        {
            QuantityLength first = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_FeetToFeet_DifferentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.IsFalse(first.Equals(second));
        }

        [Test]
        public void testEquality_InchToInch_DifferentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(2.0, LengthUnit.Inch);
            Assert.IsFalse(first.Equals(second));
        }

        [Test]
        public void testEquality_YardToYard_SameValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_YardToYard_DifferentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength second = new QuantityLength(2.0, LengthUnit.Yard);
            Assert.IsFalse(first.Equals(second));
        }

        [Test]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength second = new QuantityLength(3.0, LengthUnit.Feet);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_FeetToYard_EquivalentValue()
        {
            QuantityLength first = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_YardToInches_EquivalentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength second = new QuantityLength(36.0, LengthUnit.Inch);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_InchesToYard_EquivalentValue()
        {
            QuantityLength first = new QuantityLength(36.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_YardToFeet_NonEquivalentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength second = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.IsFalse(first.Equals(second));
        }

        [Test]
        public void testEquality_centimetersToInches_EquivalentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Centimeter);
            QuantityLength second = new QuantityLength(0.393701, LengthUnit.Inch);
            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void testEquality_centimetersToFeet_NonEquivalentValue()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Centimeter);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(first.Equals(second));
        }

        [Test]
        public void testEquality_MultiUnit_TransitiveProperty()
        {
            QuantityLength yardLength = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength feetLength = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength inchLength = new QuantityLength(36.0, LengthUnit.Inch);

            Assert.IsTrue(yardLength.Equals(feetLength));
            Assert.IsTrue(feetLength.Equals(inchLength));
            Assert.IsTrue(yardLength.Equals(inchLength));
        }

        [Test]
        public void testEquality_AllUnits_ComplexScenario()
        {
            QuantityLength yardLength = new QuantityLength(2.0, LengthUnit.Yard);
            QuantityLength feetLength = new QuantityLength(6.0, LengthUnit.Feet);
            QuantityLength inchLength = new QuantityLength(72.0, LengthUnit.Inch);

            Assert.IsTrue(yardLength.Equals(feetLength));
            Assert.IsTrue(feetLength.Equals(inchLength));
        }

        [Test]
        public void testEquality_YardSameReference()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.IsTrue(first.Equals(first));
        }

        [Test]
        public void testEquality_YardNullComparison()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.IsFalse(first.Equals(null));
        }

        [Test]
        public void testEquality_CentimetersSameReference()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Centimeter);
            Assert.IsTrue(first.Equals(first));
        }

        [Test]
        public void testEquality_CentimetersNullComparison()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Centimeter);
            Assert.IsFalse(first.Equals(null));
        }

        [Test]
        public void testEquality_DifferentType()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(first.Equals("1.0"));
        }

        [Test]
        public void testConversion_FeetToInches()
        {
            QuantityLength quantity = new QuantityLength(1.0, LengthUnit.Feet);
            double result = quantity.ConvertTo(LengthUnit.Inch);
            Assert.AreEqual(12.0, result);
        }

        [Test]
        public void testConversion_InchesToFeet()
        {
            QuantityLength quantity = new QuantityLength(24.0, LengthUnit.Inch);
            double result = quantity.ConvertTo(LengthUnit.Feet);
            Assert.AreEqual(2.0, result);
        }

        [Test]
        public void testConversion_YardsToInches()
        {
            QuantityLength quantity = new QuantityLength(1.0, LengthUnit.Yard);
            double result = quantity.ConvertTo(LengthUnit.Inch);
            Assert.AreEqual(36.0, result);
        }

        [Test]
        public void testConversion_InchesToYards()
        {
            QuantityLength quantity = new QuantityLength(72.0, LengthUnit.Inch);
            double result = quantity.ConvertTo(LengthUnit.Yard);
            Assert.AreEqual(2.0, result);
        }

        [Test]
        public void testConversion_CentimetersToInches()
        {
            QuantityLength quantity = new QuantityLength(2.54, LengthUnit.Centimeter);
            double result = quantity.ConvertTo(LengthUnit.Inch);
            // 2.54 cm is approx 1 inch. 
            // 2.54 * 0.393701 = 1.00000054. Rounding logic in class handles 5 decimals.
            Assert.AreEqual(1.0, result, 0.001);
        }

        [Test]
        public void testConversion_FeetToYard()
        {
            QuantityLength quantity = new QuantityLength(6.0, LengthUnit.Feet);
            double result = quantity.ConvertTo(LengthUnit.Yard);
            Assert.AreEqual(2.0, result);
        }

        [Test]
        public void testConversion_ZeroValue()
        {
            QuantityLength quantity = new QuantityLength(0.0, LengthUnit.Feet);
            double result = quantity.ConvertTo(LengthUnit.Inch);
            Assert.AreEqual(0.0, result);
        }

        [Test]
        public void testConversion_NegativeValue()
        {
            QuantityLength quantity = new QuantityLength(-1.0, LengthUnit.Feet);
            double result = quantity.ConvertTo(LengthUnit.Inch);
            Assert.AreEqual(-12.0, result);
        }

        [Test]
        public void testConversion_SameUnit_ReturnsSameValue()
        {
            QuantityLength quantity = new QuantityLength(5.5, LengthUnit.Feet);
            double result = quantity.ConvertTo(LengthUnit.Feet);
            Assert.AreEqual(5.5, result);
        }

        [Test]
        public void testConversion_RoundTrip_PreservesValue()
        {
            // Convert 10 Feet -> Yards -> Feet
            QuantityLength start = new QuantityLength(10.0, LengthUnit.Feet);
            double yards = start.ConvertTo(LengthUnit.Yard);

            QuantityLength mid = new QuantityLength(yards, LengthUnit.Yard);
            double feet = mid.ConvertTo(LengthUnit.Feet);

            Assert.AreEqual(10.0, feet, 0.0001);
        }

        [Test]
        public void testConversion_NaN_ThrowsException()
        {
            QuantityLength quantity = new QuantityLength(double.NaN, LengthUnit.Feet);
            Assert.Throws<ArgumentException>(() => quantity.ConvertTo(LengthUnit.Inch));
        }

        [Test]
        public void testConversion_Infinity_ThrowsException()
        {
            QuantityLength quantity = new QuantityLength(double.PositiveInfinity, LengthUnit.Feet);
            Assert.Throws<ArgumentException>(() => quantity.ConvertTo(LengthUnit.Inch));
        }


        [Test]
        public void testAddition_SameUnit_FeetPlusFeet()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(2.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(3.0, LengthUnit.Feet)));
        }

        [Test]
        public void testAddition_SameUnit_InchPlusInch()
        {
            QuantityLength first = new QuantityLength(6.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(6.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(12.0, LengthUnit.Inch)));
        }

        [Test]
        public void testAddition_CrossUnit_FeetPlusInches()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(2.0, LengthUnit.Feet)));
        }

        [Test]
        public void testAddition_CrossUnit_InchPlusFeet()
        {
            QuantityLength first = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(24.0, LengthUnit.Inch)));
        }

        [Test]
        public void testAddition_CrossUnit_YardPlusFeet()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength second = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(2.0, LengthUnit.Yard)));
        }

        [Test]
        public void testAddition_CrossUnit_CentimeterPlusInch()
        {
            QuantityLength first = new QuantityLength(2.54, LengthUnit.Centimeter);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(5.08, LengthUnit.Centimeter)));
        }

        [Test]
        public void testAddition_Commutativity()
        {
            QuantityLength feet = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength inches = new QuantityLength(12.0, LengthUnit.Inch);

            QuantityLength result1 = feet.Add(inches);
            QuantityLength result2 = inches.Add(feet);

            Assert.IsTrue(result1.Equals(result2));
        }

        [Test]
        public void testAddition_WithZero()
        {
            QuantityLength first = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(0.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(5.0, LengthUnit.Feet)));
        }

        [Test]
        public void testAddition_NegativeValues()
        {
            QuantityLength first = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(-2.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(3.0, LengthUnit.Feet)));
        }

        [Test]
        public void testAddition_NullSecondOperand()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => first.Add(null!));
        }

        [Test]
        public void testAddition_LargeValues()
        {
            QuantityLength first = new QuantityLength(1000000.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(1000000.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(2000000.0, LengthUnit.Feet)));
        }

        [Test]
        public void testAddition_SmallValues()
        {
            QuantityLength first = new QuantityLength(0.001, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(0.002, LengthUnit.Feet);
            QuantityLength result = first.Add(second);

            Assert.IsTrue(result.Equals(new QuantityLength(0.003, LengthUnit.Feet)));
        }


        [Test]
        public void testAddition_ExplicitTargetUnit_Feet()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second, LengthUnit.Feet);

            Assert.IsTrue(result.Equals(new QuantityLength(2.0, LengthUnit.Feet)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Inches()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second, LengthUnit.Inch);

            Assert.IsTrue(result.Equals(new QuantityLength(24.0, LengthUnit.Inch)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Yards()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second, LengthUnit.Yard);

            Assert.IsTrue(result.Equals(new QuantityLength(0.66667, LengthUnit.Yard)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Centimeters()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second, LengthUnit.Centimeter);

            Assert.IsTrue(result.Equals(new QuantityLength(5.08, LengthUnit.Centimeter)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_SameAsFirstOperand()
        {
            QuantityLength first = new QuantityLength(2.0, LengthUnit.Yard);
            QuantityLength second = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second, LengthUnit.Yard);

            Assert.IsTrue(result.Equals(new QuantityLength(3.0, LengthUnit.Yard)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_SameAsSecondOperand()
        {
            QuantityLength first = new QuantityLength(2.0, LengthUnit.Yard);
            QuantityLength second = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second, LengthUnit.Feet);

            Assert.IsTrue(result.Equals(new QuantityLength(9.0, LengthUnit.Feet)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_Commutativity()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(12.0, LengthUnit.Inch);

            QuantityLength result1 = first.Add(second, LengthUnit.Yard);
            QuantityLength result2 = second.Add(first, LengthUnit.Yard);

            Assert.IsTrue(result1.Equals(result2));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_WithZero()
        {
            QuantityLength first = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(0.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second, LengthUnit.Yard);

            Assert.IsTrue(result.Equals(new QuantityLength(1.66667, LengthUnit.Yard)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_NegativeValues()
        {
            QuantityLength first = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(-2.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second, LengthUnit.Inch);

            Assert.IsTrue(result.Equals(new QuantityLength(36.0, LengthUnit.Inch)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_LargeToSmallScale()
        {
            QuantityLength first = new QuantityLength(1000.0, LengthUnit.Feet);
            QuantityLength second = new QuantityLength(500.0, LengthUnit.Feet);
            QuantityLength result = first.Add(second, LengthUnit.Inch);

            Assert.IsTrue(result.Equals(new QuantityLength(18000.0, LengthUnit.Inch)));
        }

        [Test]
        public void testAddition_ExplicitTargetUnit_SmallToLargeScale()
        {
            QuantityLength first = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength second = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = first.Add(second, LengthUnit.Yard);

            Assert.IsTrue(result.Equals(new QuantityLength(0.66667, LengthUnit.Yard)));
        }
    }
}