using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services
{
    public class CalculatorHttpService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ILogger<CalculatorHttpService> logger) : ICalculatorHttpService
    {

        private void GetHeaderValue()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            string token = httpContextAccessor.HttpContext.Session.Get<User>(Constants.SessioKey)?.Token ?? "";

            headers.Add("Authorization", $"Bearer {token}");

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    if (!httpClient.DefaultRequestHeaders.Contains(item.Key))
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
            }
        }

        public async Task<AuthResponse> AuthAsync(AuthRequest authRequest)
        {
            var requestData = JsonSerializer.Serialize(authRequest);

            var content = new StringContent(requestData, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));

            GetHeaderValue();

            var response = await httpClient.PostAsync("api/auth", content);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Cannot authenticate user, status code: {response.StatusCode}");

                throw new Exception($"Cannot authenticate user, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<AuthResponse>() ?? new();

        }

        public async Task<AuthResponse> RegisterAsync(AuthRequest authRequest)
        {
            var requestData = JsonSerializer.Serialize(authRequest);

            var content = new StringContent(requestData, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));

            var response = await httpClient.PostAsync("api/auth/register", content);

            // if (!response.IsSuccessStatusCode)
            // {
            //     logger.LogError($"Cannot authenticate user, status code: {response.StatusCode}");

            //     throw new Exception($"Cannot authenticate user, status code: {response.StatusCode}");
            // }

            return await response.Content.ReadFromJsonAsync<AuthResponse>() ?? new();

        }


        public async Task<PostalCodeResponse> GetPostalCodesAsync()
        {
            GetHeaderValue();

            var response = await httpClient.GetAsync("api/postalcode");

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Cannot fetch postal codes, status code: {response.StatusCode}");

                throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<PostalCodeResponse>() ?? new();
        }

        public async Task<CalculatorHistoryResponse> GetHistoryAsync()
        {
            GetHeaderValue();

            var response = await httpClient.GetAsync("api/calculator/history");

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Cannot fetch tax history, status code: {response.StatusCode}");

                throw new Exception($"Cannot fetch tax history, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<CalculatorHistoryResponse>() ?? new();
        }

        public async Task<CalculateResponse> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            var requestData = JsonSerializer.Serialize(calculationRequest);

            var content = new StringContent(requestData, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));

            GetHeaderValue();

            var response = await httpClient.PostAsync("api/calculator/calculate-tax", content);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Cannot calculate tax, status code: {response.StatusCode}");

                throw new Exception($"Cannot calculate tax, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<CalculateResponse>() ?? new();
        }
    }
}