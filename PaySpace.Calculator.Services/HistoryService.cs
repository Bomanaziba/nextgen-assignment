using System.Net;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;

using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Response;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services
{
    internal sealed class HistoryService(CalculatorContext context, IMapper mapper) : IHistoryService
    {
        public async Task AddAsync(CalculatorHistory history)
        {
            history.Timestamp = DateTime.Now;

            await context.AddAsync(history);
            await context.SaveChangesAsync();
        }

        public async Task<CalculatorHistoryResponse> GetHistoryAsync()
        {
            var repo = await context.Set<CalculatorHistory>()
                .OrderByDescending(_ => _.Timestamp)
                .ToListAsync();

            if (repo == null || !repo.Any() || repo.Count <= 0)
            {
                return new CalculatorHistoryResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    ResponseCode = SystemCodes.DataNotFound,
                    Message = "No Tax history"
                };
            }

            return new CalculatorHistoryResponse
            {
                History = mapper.Map<List<CalculatorHistoryDto>>(repo),
                HttpStatusCode = (int)HttpStatusCode.OK,
                ResponseCode = SystemCodes.Successful,
                Message = "SUCCESSFUL"
            };
        }
    }
}