
using System.Net;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.Services;

public class TaxCalculatorService(IPostalCodeService postalCodeService, 
    IFacadeEngine facadeEngine,
     ICalculatorSettingsService calculatorSettingsService, 
    IHistoryService historyService,
    ILogger<TaxCalculatorService> logger) : ITaxCalculatorService
{

    public async Task<CalculateTaxResponse> CaculateTax(string? postalCode, decimal income)
    {
        try
        {
            if(string.IsNullOrEmpty(postalCode))
            {
                return new CalculateTaxResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    ResponseCode = SystemCodes.InvalidPostalCode,
                    Message = "Invalid postal code."
                };
            }

            if(income < 0)
            {
                return new CalculateTaxResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    ResponseCode = SystemCodes.InvalidIncome,
                    Message = "Invalid income."
                };
            }

            PostalCode postalTax = await ProcessPostalCalculatorType(postalCode);

            if(postalTax == null)
            {
                return new CalculateTaxResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    ResponseCode = SystemCodes.DataNotFound,
                    Message = "Tax for postal code not found."
                };
            }

            CalculatorType calculatorType = postalTax.Calculator;

            decimal tax = await facadeEngine.ExecuteAsync(calculatorType, income);

            await ProcessStoreTaxHistory(tax, calculatorType, postalCode, income);

            return new CalculateTaxResponse
            {
                Tax = tax,
                Calculator = calculatorType.ToString(),
                HttpStatusCode = (int)HttpStatusCode.OK,
                ResponseCode = SystemCodes.Successful,
                Message = "SUCCESSFUL"
            };
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }

        async Task ProcessStoreTaxHistory(decimal tax, CalculatorType calculatorType, string postalCode, decimal income)
        {
            await historyService.AddAsync(new CalculatorHistory
            {
                Tax = tax,
                Calculator = calculatorType,
                PostalCode = postalCode ?? "Unknown",
                Income = income
            });
        }

        async Task<PostalCode> ProcessPostalCalculatorType(string postalCode)
        {
            var repo = await postalCodeService.GetPostalCodesAsync();

            return repo?.Where(p => p.Code == postalCode).FirstOrDefault();
        }
    }

}