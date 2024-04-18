

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Common;

namespace PaySpace.Calculator.Services;

public class SecurityUtils
{

    public static (string password, string salt) HashPassword(string password)
    {
        SecureHashObj obj = HashSecuredData(password, Utils.SaltLength, Utils.SecureSHA512);

        return (obj.SecureHash, obj.Salt);
    }

    public static bool PasswordCheck(string password, string storePassword, string salt)
    {
        return CheckSHA512.IsHashSecureDataEqual(password, storePassword, salt);
    }

    public static SecureHashObj HashSecuredData(string data, int saltLength, HashAlgorithm hashAlgorithm)
    {
        byte[] saltBytes = GenerateRandomCryptographicBytes(saltLength);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        List<byte> dataWithSalt = new List<byte>();
        dataWithSalt.AddRange(dataBytes);
        dataWithSalt.AddRange(saltBytes);
        byte[] resultHash = hashAlgorithm.ComputeHash(dataWithSalt.ToArray());
        return new SecureHashObj(Convert.ToBase64String(saltBytes), Convert.ToBase64String(dataBytes), Convert.ToBase64String(resultHash));
    }

    public static SecureHashObj HashSecuredDataKnownSalt(string data, string salt, HashAlgorithm hashAlgorithm)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        List<byte> dataWithSalt = new List<byte>();
        dataWithSalt.AddRange(dataBytes);
        dataWithSalt.AddRange(saltBytes);
        byte[] resultHash = hashAlgorithm.ComputeHash(dataWithSalt.ToArray());
        return new SecureHashObj(Convert.ToBase64String(saltBytes), Convert.ToBase64String(dataBytes), Convert.ToBase64String(resultHash));
    }

    private static byte[] GenerateRandomCryptographicBytes(int keyLength)
    {
        RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
        byte[] randomBytes = new byte[keyLength];
        rNGCryptoServiceProvider.GetBytes(randomBytes);
        return randomBytes;
    }
}

public class SecureHashObj
{
    public SecureHashObj(string salt, string data, string secureData)
    {
        Salt = salt;
        Data = data;
        SecureHash = secureData;
    }

    public string Salt { get; set; }
    public string Data { get; set; }
    public string SecureHash { get; set; }
}

public class CheckSHA512
{
    public static bool IsHashSecureDataEqual(string data, string secureData, string salt)
    {
        SecureHashObj secure = SecurityUtils.HashSecuredDataKnownSalt(data, salt, SHA512.Create());

        return secure.SecureHash == secureData;
    }
}
