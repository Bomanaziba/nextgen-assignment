using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.ViewModel;

namespace PaySpace.Calculator.Web.Controllers
{
    public class CalculatorController(ICalculatorService calculatorService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var calculatorView = await calculatorService.GetCalculateTaxView();

            return this.View(calculatorView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Index(CalculateRequestViewModel request)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await calculatorService.ProcessCalculateTax(request);

                    return this.RedirectToAction(nameof(this.History));
                }
                catch (Exception e)
                {
                    this.ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            var calculatorViewModel = await calculatorService.GetCalculateTaxView(request);

            return this.View(calculatorViewModel);
        }

        public async Task<IActionResult> History()
        {
            var taxHistoryViewModel = await calculatorService.GetCalculatorHistoryView();

            return this.View(taxHistoryViewModel);
        }

    }
}