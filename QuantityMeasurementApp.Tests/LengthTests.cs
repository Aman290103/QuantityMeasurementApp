
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
    }
}
