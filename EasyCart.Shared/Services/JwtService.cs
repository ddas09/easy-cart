using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using EasyCart.Shared.Services.Contracts;
using EasyCart.Shared.Models.Configurations;

namespace EasyCart.Shared.Services;

public class JwtService : IJwtService
{
    public IEnumerable<Claim> GetClaims(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Parse the token to extract the claims without validating the token (only used for extracting data)
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken?.Claims ?? [];
        }
        catch
        {
            return [];
        }
    }

    public string GenerateToken(TokenConfigurationModel configuration, List<Claim> claims)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.TokenSecretKey));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new
        (
            configuration.Issuer,
            configuration.Audience,
            claims,
            DateTime.Now,
            DateTime.Now.AddMinutes(configuration.ExpirationInMinutes),
            credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public bool IsValidToken(TokenConfigurationModel configuration, string token)
    {
        TokenValidationParameters validationParameters = new()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.TokenSecretKey)),
            ValidIssuer = configuration.Issuer,
            ValidAudience = configuration.Audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };

        JwtSecurityTokenHandler tokenHandler = new();
        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}