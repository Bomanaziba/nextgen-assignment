

namespace PaySpace.Calculator.Web.Services.Models;


public class HttpClientBaseResponse
{
    public int HttpStatusCode { get; set; }
    public string ResponseCode { get; set; }
    public string Message { get; set; }
}