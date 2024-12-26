namespace EasyCart.AuthService.Models.Configurations;

public class JwtConfiguration
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string AccessTokenSecret { get; set; }

    public int AccessTokenExpirationTimeInMinutes { get; set; }

    public string RefreshTokenSecret { get; set; }

    public int RefreshTokenExpirationTimeInMinutes { get; set; }
}