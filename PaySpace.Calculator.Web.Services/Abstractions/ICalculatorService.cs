
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PaySpace.Calculator.Web.Services.ViewModel;

namespace PaySpace.Calculator.Web.Services.Abstractions;

public interface ICalculatorService
{
    Task<CalculatorViewModel> GetCalculateTaxView(CalculateRequestViewModel request = default, ModelStateDictionary message = default);

    Task<CalculatorHistoryViewModel> GetCalculatorHistoryView(CalculatorHistoryViewModel request = default, ModelStateDictionary message = default);

    Task<CalculatorViewModel> ProcessCalculateTax(CalculateRequestViewModel request);
}