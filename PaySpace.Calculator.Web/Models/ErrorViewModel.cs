namespace PaySpace.Calculator.Web.Models
{
    public sealed class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}