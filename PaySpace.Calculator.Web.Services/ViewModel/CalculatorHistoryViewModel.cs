using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services.ViewModel

{
    public sealed class CalculatorHistoryViewModel
    {
        public List<CalculatorHistory>? CalculatorHistory { get; set; }

        public string ProcessingMessage { get; set; }
    }
}