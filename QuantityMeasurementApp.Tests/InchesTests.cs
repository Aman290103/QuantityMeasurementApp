using NUnit.Framework;
using QuantityMeasurementApp.Core;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class InchesTests
    {
        private Inches? first;
        private Inches? second;

        [SetUp]
        public void setup()
        {
            first = new Inches(1.0);
            second = new Inches(1.0);
        }

        [Test]
        public void GivenTwoInches_WhenValuesAreEqual_ShouldReturnTrue()
        {

            Assert.IsTrue(first.Equals(second));
        }

        [Test]
        public void GivenTwoInches_WhenValuesAreNotEqual_ShouldReturnFalse()
        {
            second = new Inches(2.0);
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