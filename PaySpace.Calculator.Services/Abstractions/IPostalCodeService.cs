using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface IPostalCodeService
    {
        Task<PostalCodesResponse> GetPostalCodes();

        Task<List<PostalCode>> GetPostalCodesAsync();

        Task<CalculatorType?> CalculatorTypeAsync(string code);
    }
}