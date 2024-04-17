
namespace PaySpace.Calculator.Services.Common;

public class BaseResponse
{
    public int HttpStatusCode { get; set; }
    public string ResponseCode { get; set; }
    public string Message { get; set; }
}