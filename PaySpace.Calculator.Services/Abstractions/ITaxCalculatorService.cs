

using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions;

public interface ITaxCalculatorService
{
    Task<(decimal tax,CalculatorType calculatorType)> CaculateTax(string? postalCode, decimal income);
}