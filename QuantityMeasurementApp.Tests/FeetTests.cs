using NUnit.Framework;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class FeetTests
    {
        private Feet? first;
        private Feet? second;

        [SetUp]
        public void setup()
        {
            first = new Feet(1.0);
            second = new Feet(1.0);
        }

        [Test]
        public void GivenTwoFeet_WhenValuesAreEqual_ShouldReturnTrue()
        {

            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void GivenTwoFeet_WhenValuesAreNotEqual_ShouldReturnFalse()
        {
            second = new Feet(2.0);
            Assert.IsFalse(first.Equals(second));
        }

        [Test]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            
            Assert.IsFalse(first.Equals(null));
        }

        [Test]
        public void GivenSameReference_WhenCompared_ShouldReturnTrue()
        {
            Assert.IsTrue(first.Equals(first));
        }

        [Test]
        public void GivenDifferentType_WhenCompared_ShouldReturnFalse()
        {
            Assert.IsFalse(first.Equals("1.0"));
        }
    }
}