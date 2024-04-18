

using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.Web.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Authorize]
public class PostalCodeController(IPostalCodeService postalCodeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> PostalCodes()
    {
        PostalCodesResponse postalCodes = await postalCodeService.GetPostalCodes();

        return this.StatusCode(postalCodes.HttpStatusCode, postalCodes);
    }
}