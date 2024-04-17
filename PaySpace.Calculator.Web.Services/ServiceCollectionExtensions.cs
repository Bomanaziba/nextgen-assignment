using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using PaySpace.Calculator.Web.Services.Abstractions;

namespace PaySpace.Calculator.Web.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCalculatorHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICalculatorHttpService, CalculatorHttpService>(p => {
                p.BaseAddress = new Uri(configuration["CalculatorSettings:ApiUrl"]);
            });
        }
    }
}