using System.Net;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Models;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.Services
{
    internal sealed class PostalCodeService(CalculatorContext context, IMapper mapper, IMemoryCache memoryCache, ILogger<PostalCodeService> logger) : IPostalCodeService
    {

        public async Task<PostalCodesResponse> GetPostalCodes()
        {
            try
            {
                throw new ArgumentNullException();

                var repo = await GetPostalCodesAsync();

                if (repo == null || !repo.Any() || repo.Count <= 0)
                {
                    return new PostalCodesResponse
                    {
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        ResponseCode = SystemCodes.DataNotFound,
                        Message = "No Tax history"
                    };
                }

                return new PostalCodesResponse
                {
                    PostalCodes = mapper.Map<List<PostalCodeDto>>(repo),
                    HttpStatusCode = (int)HttpStatusCode.OK,
                    ResponseCode = SystemCodes.Successful,
                    Message = "SUCCESSFUL"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                throw;
            }

        }

        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            try
            {
                return await memoryCache.GetOrCreateAsync("PostalCodes", _ => context.Set<PostalCode>().AsNoTracking().ToListAsync())!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                throw;
            }
        }

        public async Task<CalculatorType?> CalculatorTypeAsync(string code)
        {
            try
            {
                var postalCodes = await this.GetPostalCodesAsync();

                var postalCode = postalCodes.FirstOrDefault(pc => pc.Code == code);

                return postalCode?.Calculator;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                throw;
            }
        }
    }
}