
using PaySpace.Calculator.Services.Common;

namespace PaySpace.Calculator.Services.Response;

public sealed class AuthResponse : BaseResponse
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
    
}