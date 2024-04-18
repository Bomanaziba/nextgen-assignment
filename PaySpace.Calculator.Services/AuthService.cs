using System.Net;
using System.Reflection.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Request;
using PaySpace.Calculator.Services.Response;

namespace PaySpace.Calculator.Services;


public class AuthService(CalculatorContext context, ITokenUtil tokenUtil, ILogger<AuthService> logger) : IAuthService
{
    public async Task<AuthResponse> Register(RegisterRequest signInRequest)
    {
        try
        {
            if (signInRequest == null)
            {
                return new AuthResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    ResponseCode = SystemCodes.EmptyRequest,
                    Message = "Request is null"
                };
            }

            var repo = context.Users.Where(p => p.Username == signInRequest.Username).FirstOrDefault();

            if (repo != null)
            {
                return new AuthResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    ResponseCode = SystemCodes.InvalidIncome,
                    Message = "Username already exist"
                };
            }

            (string password, string salt) passwordCombo = SecurityUtils.HashPassword(signInRequest.Password);

            var res = await context.AddAsync(new Users
            {
                LastName = signInRequest.LastName,
                FirstName = signInRequest.FirstName,
                Username = signInRequest.Username,
                Password = passwordCombo.password,
                Salt = passwordCombo.salt
            });

            await context.SaveChangesAsync();

            return new AuthResponse
            {
                Token = await tokenUtil.Generate(res.Entity),
                Username = res.Entity.Username,
                FirstName = res.Entity.FirstName,
                LastName = res.Entity.LastName,
                HttpStatusCode = (int)HttpStatusCode.OK,
                ResponseCode = SystemCodes.Successful,
                Message = "SUCCESSFUL"
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<AuthResponse> SignIn(LoginRequest signInRequest)
    {

        try
        {
            if (signInRequest == null)
            {
                return new AuthResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    ResponseCode = SystemCodes.EmptyRequest,
                    Message = "Request is null"
                };
            }

            var repo = context.Users.Where(p => p.Username == signInRequest.Username).FirstOrDefault();

            if(repo == null)
            {
                return new AuthResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.Unauthorized,
                    ResponseCode = SystemCodes.UnauthorizedUser,
                    Message = "Username ot Password is incorrect"
                };
            }

            if (!SecurityUtils.PasswordCheck(signInRequest.Password, repo?.Password, repo.Salt))
            {
                return new AuthResponse
                {
                    HttpStatusCode = (int)HttpStatusCode.Unauthorized,
                    ResponseCode = SystemCodes.UnauthorizedUser,
                    Message = "Username ot Password is incorrect"
                };
            }

            return new AuthResponse
            {
                Token = await tokenUtil.Generate(repo),
                Username = repo.Username,
                FirstName = repo.FirstName,
                LastName = repo.LastName,
                HttpStatusCode = (int)HttpStatusCode.OK,
                ResponseCode = SystemCodes.Successful,
                Message = "SUCCESSFUL"
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }


    }
}