

using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.Services.Abstractions;

public interface ITaxCalculatorService
{
    Task<CalculateTaxResponse> CaculateTax(string? postalCode, decimal income);
}