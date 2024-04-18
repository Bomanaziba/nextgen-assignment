using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;
using System.Linq;
using System.Collections.Generic;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class ProgressiveCalculator(ICalculatorSettingsService calculatorSettingsService) : BaseCalculator, IProgressiveCalculator
    {
        
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            CalculateResult calculateResult = new CalculateResult
            {
                Calculator = Data.Models.CalculatorType.Progressive,
                Tax = await ProcessTaxCalculation(income)
            };
            
            return calculateResult;
        }

        protected override async Task<decimal> ProcessTaxCalculation(decimal income)
        {
            var calculateSettings = await calculatorSettingsService.GetSettingsAsync(Data.Models.CalculatorType.Progressive);

            var taxPercentage = calculateSettings.Where(p => p.From <= income && p.To >= income && p.RateType == Data.Models.RateType.Percentage).FirstOrDefault();

            if(taxPercentage == null)
            {
                taxPercentage = calculateSettings.Where(p => p.From <= income && p.To == default && p.RateType == Data.Models.RateType.Percentage).FirstOrDefault();
            }

            return ((taxPercentage?.Rate??0M)/100) * income;
        }
    }
}