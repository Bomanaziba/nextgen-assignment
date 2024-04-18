
using System.Security.Cryptography;
using PaySpace.Calculator.Services.Calculators;

namespace PaySpace.Calculator.Services.Common;

public class Utils
{
    public const int SaltLength = 64;
    public static HashAlgorithm SecureSHA512 = SHA512.Create();
}