using System.Security.Claims;
using EasyCart.Shared.Models.Configurations;

namespace EasyCart.Shared.Services.Contracts;

public interface IJwtService
{
    IEnumerable<Claim> GetClaims(string token);

    bool IsValidToken(TokenConfigurationModel configuration, string token);

    string GenerateToken(TokenConfigurationModel configuration, List<Claim> claims);
}