

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;
using PaySpace.Calculator.Web.Services.ViewModel;

namespace PaySpace.Calculator.Web.Services;

public class CalculatorService(ICalculatorHttpService calculatorHttpService, ILogger<CalculatorService> logger) : ICalculatorService
{

    public async Task<CalculatorViewModel> GetCalculateTaxView(CalculateRequestViewModel request = default, ModelStateDictionary message = default)
    {
        try
        {
            return await GetViewModelFactoryAsync(request, errors: message?.ModelStateError());
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

                viewResp.Errors = new List<string>{ "Request is null" };

                return viewResp;
            }

            var resp2 = await calculatorHttpService.CalculateTaxAsync(new Models.CalculateRequest
            {
                PostalCode = request.PostalCode,
                Income = request.Income
            });

            if(resp2.ResponseCode != "00")
            {
                var viewResp = await GetCalculateTaxView(request);

                viewResp.Errors = new List<string>{ resp2.Message };

                return viewResp;
            }

            return await GetViewModelFactoryAsync(request);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }

    public async Task<CalculatorHistoryViewModel> GetCalculatorHistoryView(CalculatorHistoryViewModel calculatorHistoryViewModel = default, ModelStateDictionary message = default)
    {
        try
        {
            var history = await calculatorHttpService.GetHistoryAsync();


            if (history == null || history.ResponseCode != "00")
            {
                return new CalculatorHistoryViewModel
                {
                    CalculatorHistory = new List<CalculatorHistory>(),
                    Errors = new List<string>{history?.Message}
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

    private async Task<CalculatorViewModel> GetViewModelFactoryAsync(CalculateRequestViewModel? request = null, string redirectUrl = "", List<string> message = default, List<string> errors = default)
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
                    PostalCode = request?.PostalCode ?? string.Empty,
                    Errors = errors,
                    ProcessingMessages = message,
                    RedirectUrl = redirectUrl
                };
            }

            var postalCodes = postalCodeResponse.PostalCodes;

            return new CalculatorViewModel
            {
                PostalCodes = postalCodes,
                PostalCodesDropDown = postalCodes?.Select(u => new SelectListItem { Text = u.Code, Value = u.Code, Selected = (u.Code == request?.PostalCode) }).ToList() ?? new List<SelectListItem>(),
                Income = request?.Income ?? 0M,
                PostalCode = request?.PostalCode ?? string.Empty,
                Errors = errors,
                ProcessingMessages = message,
                RedirectUrl = redirectUrl
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }


}