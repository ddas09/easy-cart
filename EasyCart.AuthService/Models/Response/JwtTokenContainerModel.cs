namespace EasyCart.AuthService.Models.Response;

public class JwtTokenContainerModel
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}