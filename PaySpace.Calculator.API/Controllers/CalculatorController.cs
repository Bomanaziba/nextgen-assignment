using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public sealed class CalculatorController(
        ITaxCalculatorService taxCalculatorService,
        IHistoryService historyService)
        : ControllerBase
    {
        [HttpPost("calculate-tax")]
        public async Task<IActionResult> Calculate(CalculateRequest request)
        {
            CalculateTaxResponse taxComputation = await taxCalculatorService.CaculateTax(request.PostalCode, request.Income); 

            return this.StatusCode(taxComputation.HttpStatusCode, taxComputation);
        }

        [HttpGet("history")]
        public async Task<IActionResult> History()
        {
            CalculatorHistoryResponse history = await historyService.GetHistoryAsync();

            return this.StatusCode(history.HttpStatusCode, history);
        }
    }
}