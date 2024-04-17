using Microsoft.EntityFrameworkCore;

using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Services
{
    internal sealed class HistoryService(CalculatorContext context) : IHistoryService
    {
        public async Task AddAsync(CalculatorHistory history)
        {
            history.Timestamp = DateTime.Now;

            await context.AddAsync(history);
            await context.SaveChangesAsync();
        }

        public Task<List<CalculatorHistory>> GetHistoryAsync()
        {
            return context.Set<CalculatorHistory>()
                .OrderByDescending(_ => _.Timestamp)
                .ToListAsync();
        }
    }
}