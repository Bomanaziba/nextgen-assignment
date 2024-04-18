
namespace PaySpace.Calculator.Web.Services.Models;

public sealed class AuthResponse : HttpClientBaseResponse
{
    public string Token { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}