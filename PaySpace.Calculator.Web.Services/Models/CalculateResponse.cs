


namespace PaySpace.Calculator.Web.Services.Models;

public sealed class CalculateResponse : HttpClientBaseResponse
{
    public string Calculator { get; set; }

    public decimal Tax { get; set; }
}