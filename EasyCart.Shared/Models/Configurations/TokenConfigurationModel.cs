namespace EasyCart.Shared.Models.Configurations;

public class TokenConfigurationModel
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string TokenSecretKey { get; set; }

    public int ExpirationInMinutes { get; set; }
}