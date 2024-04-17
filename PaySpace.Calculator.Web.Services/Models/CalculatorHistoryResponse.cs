
namespace  PaySpace.Calculator.Web.Services.Models;

    
public sealed class CalculatorHistoryResponse : HttpClientBaseResponse
{
    public List<CalculatorHistory> History { get; set; }
}