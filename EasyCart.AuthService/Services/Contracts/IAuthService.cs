using EasyCart.AuthService.Models.Request;
using EasyCart.AuthService.Models.Response;

namespace EasyCart.AuthService.Services.Contracts;

public interface IAuthService
{
    Task<AuthResponse> LoginUser(LoginRequest request);

    Task Logout(int userId);
    
    Task<AuthResponse> RegisterUser(RegisterRequest request);

    Task<JwtTokenContainerModel> RefreshToken(RefreshTokenRequestModel request);
}