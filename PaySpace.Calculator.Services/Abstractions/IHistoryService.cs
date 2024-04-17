using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface IHistoryService
    {
        Task<CalculatorHistoryResponse> GetHistoryAsync();

        Task AddAsync(CalculatorHistory calculatorHistory);
    }
}