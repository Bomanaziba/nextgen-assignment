
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.ViewModel;

namespace PaySpace.Calculator.Web.Controllers;

public class HomeController(IAuthService authService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var calculatorView = await authService.GetAuthView();

        return this.View(calculatorView);
    }

    [HttpPost]
    [ValidateAntiForgeryToken()]
    public async Task<IActionResult> Index(AuthViewModel request)
    {
        if (this.ModelState.IsValid)
        {
            try
            {
                var resp = await authService.ProcessAuthentication(request);

                if (resp?.Errors != null && resp.Errors.Any() || resp?.ProcessingMessages != null && resp.ProcessingMessages.Any())
                {
                    return this.View(nameof(this.Index), resp);
                }

                return Redirect(resp.RedirectUrl ?? "");
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        var respMessage = await authService.GetAuthView(request, this.ModelState);

        return this.View(respMessage);
    }

    public async Task<IActionResult> Register()
    {
        var calculatorView = await authService.GetAuthView();

        return this.View(calculatorView);
    }

    [HttpPost]
    [ValidateAntiForgeryToken()]
    public async Task<IActionResult> Register(AuthViewModel request)
    {
        if (this.ModelState.IsValid)
        {
            try
            {
                var resp = await authService.ProcessRegister(request);

                if (resp?.Errors != null && resp.Errors.Any() || resp?.ProcessingMessages != null && resp.ProcessingMessages.Any())
                {
                    return this.View(nameof(this.Register), resp);
                }

                return Redirect(resp.RedirectUrl ?? "");
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        var respMessage = await authService.GetAuthView(request, this.ModelState);

        return this.View(respMessage);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> SignOut()
    {
        var resp = await authService.ProcessLogOut();

        if (resp?.Errors != null && resp.Errors.Any())
        {
            return this.View(nameof(this.Index),resp);
        }

        return this.RedirectToAction(nameof(this.Index));
    }



}