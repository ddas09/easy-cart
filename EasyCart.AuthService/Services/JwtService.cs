using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using EasyCart.AuthService.Models.Response;
using EasyCart.AuthService.Services.Contracts;
using EasyCart.AuthService.Models.Configurations;

namespace EasyCart.AuthService.Services;

public class JwtService(IOptions<JwtConfiguration> configuration) : IJwtService
{
    private readonly JwtConfiguration _configuration = configuration.Value;

    public JwtTokenContainerModel GetTokens(UserInformation user)
    {
        var claims = new List<Claim>
        {
            new("email", user.Email),
            new("id", user.Id.ToString())
        };

        var accessToken = this.SignJwt(_configuration.AccessTokenSecret, _configuration.AccessTokenExpirationTimeInMinutes, claims);
        var refreshToken = this.SignJwt(_configuration.RefreshTokenSecret, _configuration.RefreshTokenExpirationTimeInMinutes, claims);

        var tokenContainer = new JwtTokenContainerModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };

        return tokenContainer;
    }

    public bool IsValidAccessToken(string token)
    {
        return ValidateToken(token, _configuration.AccessTokenSecret);
    }

    public bool IsValidRefreshToken(string token)
    {
        return ValidateToken(token, _configuration.RefreshTokenSecret);
    }

    private string SignJwt(string tokenSecret, int expirationTimeInMinutes, List<Claim>? claims = null)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new
        (
            _configuration.Issuer,
            _configuration.Audience,
            claims,
            DateTime.Now,
            DateTime.Now.AddMinutes(expirationTimeInMinutes),
            credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private bool ValidateToken(string token, string tokenSecret)
    {
        TokenValidationParameters validationParameters = new()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret)),
            ValidIssuer = _configuration.Issuer,
            ValidAudience = _configuration.Audience,
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