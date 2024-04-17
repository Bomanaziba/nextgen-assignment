

using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Response;

public sealed class PostalCodesResponse : BaseResponse
{
    public List<PostalCodeDto> PostalCodes { get; set; }
}