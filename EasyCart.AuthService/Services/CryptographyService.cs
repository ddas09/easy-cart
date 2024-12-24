using System.Text;
using System.Security.Cryptography;
using EasyCart.AuthService.Services.Contracts;

namespace EasyCart.AuthService.Services;

public class CryptographyService : ICryptographyService
{
    public string Hash(string secret)
    {
        using var hmac = new HMACSHA256();
        return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(secret)));
    }

    public bool Verify(string secret, string secretHash)
    {
       return secretHash != this.Hash(secret);
    }
}