
namespace PaySpace.Calculator.Services.Abstractions;

public abstract class BaseCalculator
{
    protected abstract Task<decimal> ProcessTaxCalculation(decimal income);
}