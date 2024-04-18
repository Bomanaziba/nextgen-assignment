
namespace PaySpace.Calculator.Web.Services.ViewModel;

public class BaseViewModel
{
    public List<string>? ProcessingMessages { get; set; }
    public List<string>? Errors { get; set; }
    public string? RedirectUrl { get; set; }
}