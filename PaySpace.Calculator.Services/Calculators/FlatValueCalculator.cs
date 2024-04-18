using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatValueCalculator(ICalculatorSettingsService calculatorSettingsService) : BaseCalculator, IFlatValueCalculator
    {
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            CalculateResult calculateResult = new CalculateResult
            {
                Calculator = Data.Models.CalculatorType.FlatValue,
                Tax = await ProcessTaxCalculation(income)
            };
            
            return calculateResult;
        }

        protected override async Task<decimal> ProcessTaxCalculation(decimal income)
        {
            var calculateSettings = await calculatorSettingsService.GetSettingsAsync(Data.Models.CalculatorType.FlatValue);

            var taxPercentage = calculateSettings.Where(p => p.From < income && p.To > income && p.RateType == Data.Models.RateType.Percentage).FirstOrDefault();

            if(taxPercentage != null)
            {
                return ((taxPercentage?.Rate??0M)/100) * income;
            }
            
            var fixValue = calculateSettings.Where(p => p.From <= income && p.RateType == Data.Models.RateType.Amount).FirstOrDefault();

            return fixValue?.Rate??default;
        }
    }
}