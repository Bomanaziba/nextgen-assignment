using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class ProgressiveCalculatorTests
    {
        Mock<IProgressiveCalculator> _progressiveCalculator;

        [SetUp]
        public void Setup()
        {
            _progressiveCalculator = new Mock<IProgressiveCalculator>(MockBehavior.Strict);
        }

        [TestCase(-1, 0)]
        [TestCase(50, 5)]
        [TestCase(8350.1, 835.01)]
        [TestCase(8351, 835)]
        [TestCase(33951, 4674.85)]
        [TestCase(82251, 16749.60)]
        [TestCase(171550, 41753.32)]
        [TestCase(999999, 327681.79)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            _progressiveCalculator.Setup(p => p.CalculateAsync(income).Result).Returns(new CalculateResult
            {
                Tax = expectedTax,
                Calculator = Data.Models.CalculatorType.FlatRate
            });

            // Act
            var result = await _progressiveCalculator.Object.CalculateAsync(income);
            Assert.That(result.Tax,Is.EqualTo(expectedTax));

            // Assert
            _progressiveCalculator.VerifyAll();
        }
    }
}