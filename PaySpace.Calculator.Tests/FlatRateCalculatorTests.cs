using Moq;

using NUnit.Framework;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatRateCalculatorTests
    {
        [SetUp]
        public void Setup()
        {            
        }

        [TestCase(999999, 174999.825)]
        [TestCase(1000, 175)]
        [TestCase(5, 0.875)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange

            // Act


            // Assert
        }
    }
}
