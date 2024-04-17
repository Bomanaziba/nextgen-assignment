
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Services;

public class TaxCalculatorService(IPostalCodeService postalCodeService, ICalculatorSettingsService calculatorSettingsService, ILogger<TaxCalculatorService> logger) : ITaxCalculatorService
{

    public async Task<(decimal tax, CalculatorType calculatorType)> CaculateTax(string? postalCode, decimal income)
    {
        try
        {
            decimal tax = default; 
            CalculatorType calculatorType = default;

            var repo = await postalCodeService.GetPostalCodesAsync();

            var postalTax = repo.Where(p => p.Code == postalCode).FirstOrDefault();

            if(postalTax == null)
            {
                return (tax,calculatorType);
            }

            calculatorType = postalTax.Calculator;

            var calculateSettings = await calculatorSettingsService.GetSettingsAsync(postalTax.Calculator);

            var taxPercentage = calculateSettings.Where(p => p.From <= income && p.To >= income).FirstOrDefault();

            tax = ((taxPercentage?.Rate??0M)/100) * income;

            return (tax,calculatorType);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }
}