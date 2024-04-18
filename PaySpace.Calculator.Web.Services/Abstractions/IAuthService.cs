
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PaySpace.Calculator.Web.Services.ViewModel;

namespace PaySpace.Calculator.Web.Services.Abstractions;

public interface IAuthService
{
    Task<AuthViewModel> GetAuthView(AuthViewModel request = default, ModelStateDictionary message = default);
    Task<AuthViewModel> ProcessAuthentication(AuthViewModel request = default, ModelStateDictionary message = default);
    Task<AuthViewModel> ProcessRegister(AuthViewModel request = default, ModelStateDictionary message = default);
    Task<AuthViewModel> ProcessLogOut(AuthViewModel request = default, ModelStateDictionary message = default);
}