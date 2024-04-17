
using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Response;

public sealed class CalculatorHistoryResponse : BaseResponse
{
    public IList<CalculatorHistoryDto> History { get; set; }
}