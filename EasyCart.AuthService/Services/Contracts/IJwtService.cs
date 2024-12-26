using EasyCart.AuthService.Models.Response;

namespace EasyCart.AuthService.Services.Contracts;

public interface IJwtService
{
    bool IsValidAccessToken(string token);

    bool IsValidRefreshToken(string token);

    JwtTokenContainerModel GetTokens(UserInformation user);
}