using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Models
{
    public sealed class CalculateResult
    {
        public CalculatorType Calculator { get; set; }

        public decimal Tax { get; set; }
    }
}