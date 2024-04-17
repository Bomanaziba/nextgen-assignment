using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Services
{
    internal sealed class PostalCodeService(CalculatorContext context, IMemoryCache memoryCache) : IPostalCodeService
    {
        public Task<List<PostalCode>> GetPostalCodesAsync()
        {
            return memoryCache.GetOrCreateAsync("PostalCodes", _ => context.Set<PostalCode>().AsNoTracking().ToListAsync())!;
        }

        public async Task<CalculatorType?> CalculatorTypeAsync(string code)
        {
            var postalCodes = await this.GetPostalCodesAsync();

            var postalCode = postalCodes.FirstOrDefault(pc => pc.Code == code);

            return postalCode?.Calculator;
        }
    }
}