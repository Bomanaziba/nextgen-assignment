using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services.Abstractions
{
    public interface ICalculatorHttpService
    {
        Task<AuthResponse> AuthAsync(AuthRequest authRequest);

        Task<AuthResponse> RegisterAsync(AuthRequest authRequest);
        
        Task<PostalCodeResponse> GetPostalCodesAsync();

        Task<CalculatorHistoryResponse> GetHistoryAsync();

        Task<CalculateResponse> CalculateTaxAsync(CalculateRequest calculationRequest);
    }
}