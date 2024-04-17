namespace PaySpace.Calculator.Web.Services.Models
{
    public sealed class CalculateRequest
    {
        public string? PostalCode { get; set; }

        public decimal Income { get; set; }
    }
}