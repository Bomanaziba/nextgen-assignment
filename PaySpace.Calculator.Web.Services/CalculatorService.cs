

using System.Web.Mvc;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;
using PaySpace.Calculator.Web.Services.ViewModel;

namespace PaySpace.Calculator.Web.Services;

public class CalculatorService(ICalculatorHttpService calculatorHttpService, ILogger<CalculatorService> logger) : ICalculatorService
{

    public async Task<CalculatorViewModel> GetCalculateTaxView(CalculateRequestViewModel request = default)
    {
        try
        {
            return await GetCalculatorViewModelAsync(request);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }

    public async Task<CalculatorViewModel> ProcessCalculateTax(CalculateRequestViewModel request)
    {
        try
        {
            if (request == null)
            {
                var viewResp = await GetCalculateTaxView(request);
                viewResp.ProcessingMessage = "Request is null";

                return viewResp;
            }

            await calculatorHttpService.CalculateTaxAsync(new Models.CalculateRequest
            {
                PostalCode = request.PostalCode,
                Income = request.Income
            });

            return await GetCalculatorViewModelAsync(request);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }

    public async Task<CalculatorHistoryViewModel> GetCalculatorHistoryView(CalculatorHistoryViewModel calculatorHistoryViewModel = default)
    {
        try
        {
            var history = await calculatorHttpService.GetHistoryAsync();


            if (history == null || history.ResponseCode != "00")
            {
                return new CalculatorHistoryViewModel
                {
                    CalculatorHistory = new List<CalculatorHistory>()
                };
            }

            return new CalculatorHistoryViewModel
            {
                CalculatorHistory = history.History
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }

    private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
    {
        try
        {
            var postalCodeResponse = await calculatorHttpService.GetPostalCodesAsync();

            if (postalCodeResponse == null || postalCodeResponse?.ResponseCode != "00")
            {
                return new CalculatorViewModel
                {
                    PostalCodes = new List<PostalCode>(),
                    PostalCodesDropDown = new List<SelectListItem>(),
                    Income = request?.Income ?? 0M,
                    PostalCode = request?.PostalCode ?? string.Empty
                };
            }

            var postalCodes = postalCodeResponse.PostalCodes;

            return new CalculatorViewModel
            {
                PostalCodes = postalCodes,
                PostalCodesDropDown = postalCodes?.Select(u => new SelectListItem { Text = u.Code, Value = u.Code, Selected = (u.Code == request?.PostalCode) }).ToList() ?? new List<SelectListItem>(),
                Income = request?.Income ?? 0M,
                PostalCode = request?.PostalCode ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }
}