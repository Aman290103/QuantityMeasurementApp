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
        public void testEquality_SameReference()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            
            Assert.IsTrue(first.Equals(first));
        }

        [Test]
        public void testEquality_NullComparison()
        {
            QuantityLength first = new QuantityLength(1.0, LengthUnit.Feet);
            
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