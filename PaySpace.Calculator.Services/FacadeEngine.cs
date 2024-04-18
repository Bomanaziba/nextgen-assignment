

using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services;

public class FacadeEngine(IProgressiveCalculator progressiveCalculator, IFlatValueCalculator flatValueCalculator, IFlatRateCalculator flatRateCalculator, ILogger<FacadeEngine> logger) : IFacadeEngine
{
    public async Task<decimal> ExecuteAsync(CalculatorType calculatorType, decimal income)
    {
        CalculateResult tax;

        switch(calculatorType)
        {
            case CalculatorType.FlatRate:
                tax =  await flatRateCalculator.CalculateAsync(income);
                break;
            case CalculatorType.FlatValue:
                tax = await flatValueCalculator.CalculateAsync(income);
                break;
            case CalculatorType.Progressive:
                tax = await progressiveCalculator.CalculateAsync(income);
                break;
            default:
                tax = new();
                break;
        }

        return tax.Tax;
    }
}