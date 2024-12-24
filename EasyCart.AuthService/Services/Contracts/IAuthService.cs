using EasyCart.AuthService.Models.Request;
using EasyCart.AuthService.Models.Response;

namespace EasyCart.AuthService.Services.Contracts;

public interface IAuthService
{
    Task<AuthResponse> LoginUser(LoginRequest request);
    
    Task<AuthResponse> RegisterUser(RegisterRequest request);
}