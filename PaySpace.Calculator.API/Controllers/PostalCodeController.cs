

using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Web.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PostalCodeController(IPostalCodeService postalCodeService, 
    IMapper mapper,
    ILogger<PostalCodeController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PostalCode>>> PostalCodes()
    {
        try
        {
            var result = await postalCodeService.GetPostalCodesAsync();

            return this.Ok(mapper.Map<List<PostalCodeDto>>(result));

        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);

            return this.BadRequest(e.Message);
        }
    }
}