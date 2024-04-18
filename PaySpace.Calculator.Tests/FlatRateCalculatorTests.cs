using Moq;

using NUnit.Framework;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatRateCalculatorTests
    {
        Mock<IFlatRateCalculator> _flatRateCalculator;

        [SetUp]
        public void Setup()
        {        
             _flatRateCalculator = new Mock<IFlatRateCalculator>(MockBehavior.Strict);    
        }

        [TestCase(999999, 174999.825)]
        [TestCase(1000, 175)]
        [TestCase(5, 0.875)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            _flatRateCalculator.Setup(p => p.CalculateAsync(income).Result).Returns(new CalculateResult
            {
                Tax = expectedTax,
                Calculator = Data.Models.CalculatorType.FlatRate
            });

            // Act
            var result = await _flatRateCalculator.Object.CalculateAsync(income);
            Assert.That(result.Tax,Is.EqualTo(expectedTax));

            // Assert
            _flatRateCalculator.VerifyAll();
        }
    }
}
