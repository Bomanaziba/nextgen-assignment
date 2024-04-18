

using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions;

public interface IFacadeEngine
{
    Task<decimal> ExecuteAsync(CalculatorType calculatorType, decimal income);
}