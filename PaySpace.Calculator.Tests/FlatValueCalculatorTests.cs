using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatValueCalculatorTests
    {

        Mock<IFlatValueCalculator> _flatValueCalculator;

        [SetUp]
        public void Setup()
        {
             _flatValueCalculator = new Mock<IFlatValueCalculator>(MockBehavior.Strict); 
        }

        [TestCase(199999, 9999.95)]
        [TestCase(100, 5)]
        [TestCase(200000, 10000)]
        [TestCase(6000000, 10000)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            _flatValueCalculator.Setup(p => p.CalculateAsync(income).Result).Returns(new CalculateResult
            {
                Tax = expectedTax,
                Calculator = Data.Models.CalculatorType.FlatValue
            });

            // Act
            var result = await _flatValueCalculator.Object.CalculateAsync(income);
            Assert.That(result.Tax,Is.EqualTo(expectedTax));

            // Assert
            _flatValueCalculator.VerifyAll();
        }
    }
}