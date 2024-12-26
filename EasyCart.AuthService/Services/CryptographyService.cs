using BC = BCrypt.Net.BCrypt;
using EasyCart.AuthService.Services.Contracts;

namespace EasyCart.AuthService.Services;

public class CryptographyService : ICryptographyService
{
    public string Hash(string secret)
    {
        return BC.EnhancedHashPassword(secret);
    }

    public bool Verify(string secret, string secretHash)
    {
        return BC.EnhancedVerify(secret, secretHash);
    }
}