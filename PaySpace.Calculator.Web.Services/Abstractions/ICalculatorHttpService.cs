using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services.Abstractions
{
    public interface ICalculatorHttpService
    {
        Task<PostalCodeResponse> GetPostalCodesAsync();

        Task<CalculatorHistoryResponse> GetHistoryAsync();

        Task<CalculateResponse> CalculateTaxAsync(CalculateRequest calculationRequest);
    }
}