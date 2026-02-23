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
    }
}