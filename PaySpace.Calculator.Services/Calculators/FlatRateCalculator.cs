using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatRateCalculator(ICalculatorSettingsService calculatorSettingsService) : BaseCalculator, IFlatRateCalculator
    {
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            CalculateResult calculateResult = new CalculateResult
            {
                Calculator = Data.Models.CalculatorType.FlatRate,
                Tax = await ProcessTaxCalculation(income)
            };
            
            return calculateResult;
        }

        protected override async Task<decimal> ProcessTaxCalculation(decimal income)
        {
            var calculateSettings = await calculatorSettingsService.GetSettingsAsync(Data.Models.CalculatorType.FlatRate);

            var taxPercentage = calculateSettings.Where(p => p.RateType == Data.Models.RateType.Percentage).FirstOrDefault();

            if(taxPercentage == null)
            {
                return 0M;
            }

            return ((taxPercentage?.Rate??0M)/100) * income;
        }
    }
}