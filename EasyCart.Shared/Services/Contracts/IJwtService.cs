using System.Security.Claims;
using EasyCart.Shared.Models.Configurations;

namespace EasyCart.Shared.Services.Contracts;

public interface IJwtService
{
    bool IsValidToken(TokenConfigurationModel configuration, string token);

    string GenerateToken(TokenConfigurationModel configuration, List<Claim> claims);
}