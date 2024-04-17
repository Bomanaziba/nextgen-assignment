
using PaySpace.Calculator.Services.Common;

namespace PaySpace.Calculator.Services.Response;

public sealed class CalculateTaxResponse : BaseResponse
{
    public string Calculator { get; set; }

    public decimal Tax { get; set; }
}