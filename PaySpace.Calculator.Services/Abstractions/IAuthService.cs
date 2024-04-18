

using PaySpace.Calculator.Services.Request;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.Services.Abstractions;

public interface IAuthService
{
    Task<AuthResponse> Register(RegisterRequest signInRequest);
    Task<AuthResponse> SignIn(LoginRequest signInRequest);

}