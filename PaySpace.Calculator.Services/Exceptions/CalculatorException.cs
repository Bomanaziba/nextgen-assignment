namespace PaySpace.Calculator.Services.Exceptions
{
    public sealed class CalculatorException() : InvalidOperationException("Invalid Postal code. Calculator not found");
}