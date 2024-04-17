namespace PaySpace.Calculator.Web.Services.ViewModel
{
    public sealed class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}