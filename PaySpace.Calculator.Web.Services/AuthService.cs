using System.Security.Claims;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;
using PaySpace.Calculator.Web.Services.ViewModel;

namespace PaySpace.Calculator.Web.Services;

public class AuthService(ICalculatorHttpService calculatorHttpService, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<AuthService> logger) : IAuthService
{

    public async Task<AuthViewModel> GetAuthView(AuthViewModel request = default, ModelStateDictionary message = default)
    {
        try
        {
            return await GetViewModelFactoryAsync(request, errors: message?.ModelStateError());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }

    public async Task<AuthViewModel> ProcessAuthentication(AuthViewModel request = default, ModelStateDictionary message = default)
    {
        try
        {
            var resp = await calculatorHttpService.AuthAsync(mapper.Map<AuthRequest>(request));

            if(resp.ResponseCode != "00")
            {
                return await GetViewModelFactoryAsync(request, message: new List<string>{resp.Message});
            }

            User user = mapper.Map<User>(resp);

            httpContextAccessor.HttpContext.Session.Set(Constants.SessioKey, user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // This is optional configuration
             var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            };

            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            var res = await GetViewModelFactoryAsync(request, Constants.RedirectUrl, message?.ModelStateError());

            return res; 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }


    public async Task<AuthViewModel> ProcessRegister(AuthViewModel request = default, ModelStateDictionary message = default)
    {
        try
        {

            var req = mapper.Map<AuthRequest>(request);

            var resp = await calculatorHttpService.RegisterAsync(req);

            if(resp.ResponseCode != "00")
            {
                return await GetViewModelFactoryAsync(request, errors: new List<string>{resp.Message});
            }

            var loginInCall = await calculatorHttpService.AuthAsync(req);

            User user = mapper.Map<User>(resp);

            httpContextAccessor.HttpContext.Session.Set(Constants.SessioKey, user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // This is optional configuration
             var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            };

            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            var res = await GetViewModelFactoryAsync(request, Constants.RedirectUrl, message?.ModelStateError());

            return res; 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }


    public async Task<AuthViewModel> ProcessLogOut(AuthViewModel request = default, ModelStateDictionary message = default)
    {
        try
        {
            httpContextAccessor.HttpContext.Session.Remove(Constants.SessioKey);

            await httpContextAccessor.HttpContext.SignOutAsync();

            var res = await GetViewModelFactoryAsync(request, Constants.LoginUrl, message?.ModelStateError());

            return res;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }

    private async Task<AuthViewModel> GetViewModelFactoryAsync(AuthViewModel? request = null, string redirectUrl = "", List<string> message = default, List<string> errors = default) 
    {
        try
        {
            return new AuthViewModel
            {
                LastName = request?.LastName,
                FirstName = request?.FirstName,
                Username = request?.Username,
                Password = request?.Password,
                ProcessingMessages = message,
                RedirectUrl = redirectUrl,
                Errors = errors
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            throw;
        }
    }



}