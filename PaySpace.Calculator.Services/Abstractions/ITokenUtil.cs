
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions;

public interface ITokenUtil
{
    Task<string> Generate(Users user);
}