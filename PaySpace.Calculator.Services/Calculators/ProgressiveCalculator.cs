using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class ProgressiveCalculator : IProgressiveCalculator
    {
        public Task<CalculateResult> CalculateAsync(decimal income)
        {
            throw new NotImplementedException();
        }
    }
}