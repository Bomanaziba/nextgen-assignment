using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ICalculatorSettingsService
    {
        Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType);
    }
}