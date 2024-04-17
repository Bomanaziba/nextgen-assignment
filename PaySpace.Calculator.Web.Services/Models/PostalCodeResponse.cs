
namespace PaySpace.Calculator.Web.Services.Models;


public sealed class PostalCodeResponse : HttpClientBaseResponse
{
    public List<PostalCode> PostalCodes { get; set; }
}