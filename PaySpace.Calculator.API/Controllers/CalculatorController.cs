using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class CalculatorController(
        ILogger<CalculatorController> logger,
        ITaxCalculatorService taxCalculatorService,
        IHistoryService historyService,
        IMapper mapper)
        : ControllerBase
    {
        [HttpPost("calculate-tax")]
        public async Task<ActionResult<CalculateResult>> Calculate(CalculateRequest request)
        {
            try
            {
                (decimal tax,CalculatorType calculatorType) taxComputation = await taxCalculatorService.CaculateTax(request.PostalCode, request.Income); 

                CalculateResult result = new CalculateResult
                {
                    Tax = taxComputation.tax,
                    Calculator = taxComputation.calculatorType
                };
                
                await historyService.AddAsync(new CalculatorHistory
                {
                    Tax = result.Tax,
                    Calculator = result.Calculator,
                    PostalCode = request.PostalCode ?? "Unknown",
                    Income = request.Income
                });

                return this.Ok(mapper.Map<CalculateResultDto>(result));
            }
            catch (CalculatorException e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<CalculatorHistory>>> History()
        {
            var history = await historyService.GetHistoryAsync();

            return this.Ok(mapper.Map<List<CalculatorHistoryDto>>(history));
        }
    }
}