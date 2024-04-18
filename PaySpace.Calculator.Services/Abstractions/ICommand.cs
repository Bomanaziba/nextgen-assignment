
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions;

public interface ICommand
{
    Task<CalculateResult> CalculateAsync(decimal income);
}