using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PaySpace.Calculator.Web.Models;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Controllers
{
    public class CalculatorController(ICalculatorHttpService calculatorHttpService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var vm = await this.GetCalculatorViewModelAsync();

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Index(CalculateRequestViewModel request)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await calculatorHttpService.CalculateTaxAsync(new CalculateRequest
                    {
                        PostalCode = request.PostalCode,
                        Income = request.Income
                    });

                    return this.RedirectToAction(nameof(this.History));
                }
                catch (Exception e)
                {
                    this.ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            var vm = await this.GetCalculatorViewModelAsync(request);

            return this.View(vm);
        }

        public async Task<IActionResult> History()
        {
            return this.View(new CalculatorHistoryViewModel
            {
                CalculatorHistory = await calculatorHttpService.GetHistoryAsync()
            });
        }

        private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
        {
            try
            {
                var postalCodes = await calculatorHttpService.GetPostalCodesAsync();

                return new CalculatorViewModel
                {
                    PostalCodes = postalCodes,
                    PostalCodesDropDown = postalCodes.Select(u => new SelectListItem { Text = u.Code, Value = u.Code, Selected = (u.Code == request?.PostalCode ) }).ToList(),
                    Income = request?.Income ?? 0M,
                    PostalCode = request?.PostalCode ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}