
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Services.Common;

namespace PaySpace.Calculator.Services.Middleware;

public class ApplicationExcpetionHandler(RequestDelegate next, ILogger<ApplicationExcpetionHandler> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            var error = new BaseResponse
            {
                HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                ResponseCode = SystemCodes.UnknownError,
                Message = "An error occurred"
            };

            context.Response.StatusCode = error.HttpStatusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(error, new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            }), Encoding.UTF8);
        }
    }


}
