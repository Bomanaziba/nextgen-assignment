
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Services;


public class TokenUtil(IConfiguration configuration) : ITokenUtil
{
    public async Task<string> Generate(Users user)
    {
        try
        {
            var claims = new List<Claim>();

            claims.Add(new Claim("id", user.Id.ToString()));
            claims.Add(new Claim("name", user.Username));

            return await Token(claims, long.Parse(configuration["Jwt:Expires"]));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private async Task<string> Token(List<Claim> claims, long timeSpan)
    {
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken descriptor = new JwtSecurityToken
            (
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(timeSpan),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return handler.WriteToken(descriptor);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }


}