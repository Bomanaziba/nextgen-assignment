
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Request;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{


    [HttpPost]
    public async Task<IActionResult> SignIn(LoginRequest request)
    {
        AuthResponse auth = await authService.SignIn(request);

        return this.StatusCode(auth.HttpStatusCode, auth);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        AuthResponse auth = await authService.Register(request);

        return this.StatusCode(auth.HttpStatusCode, auth);
    }



}

