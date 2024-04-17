using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services
{
    public class CalculatorHttpService(HttpClient httpClient, ILogger<CalculatorHttpService> logger) : ICalculatorHttpService
    {
        
        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            var response = await httpClient.GetAsync("api/postalcode");
            
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Cannot fetch postal codes, status code: {response.StatusCode}");

                throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<PostalCode>>() ?? [];
        }

        public async Task<List<CalculatorHistory>> GetHistoryAsync()
        {
            var response = await httpClient.GetAsync("api/calculator/history");
            
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Cannot fetch tax history, status code: {response.StatusCode}");

                throw new Exception($"Cannot fetch tax history, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<CalculatorHistory>>() ?? [];
        }

        public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            var requestData = JsonSerializer.Serialize(calculationRequest);
            
            var content =  new StringContent(requestData, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));

            var response = await httpClient.PostAsync("api/calculator/calculate-tax", content);
            
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Cannot calculate tax, status code: {response.StatusCode}");

                throw new Exception($"Cannot calculate tax, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<CalculateResult>() ?? new();
        }
    }
}