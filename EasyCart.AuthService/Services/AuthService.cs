using AutoMapper;
using System.Security.Claims;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Exceptions;
using Microsoft.Extensions.Options;
using EasyCart.AuthService.DAL.Entities;
using EasyCart.AuthService.DAL.Contracts;
using EasyCart.Shared.Services.Contracts;
using EasyCart.AuthService.Models.Request;
using EasyCart.AuthService.Models.Response;
using EasyCart.AuthService.Services.Contracts;
using EasyCart.AuthService.Models.Configurations;

namespace EasyCart.AuthService.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyService _cryptographyService;
    private readonly AccessTokenConfiguration _accessTokenConfiguration;
    private readonly RefreshTokenConfiguration _refreshTokenConfiguration;

    public AuthService
    (
        IMapper mapper, 
        IJwtService jwtService,
        IUserRepository userRepository, 
        ICryptographyService cryptographyService,
        IOptions<AccessTokenConfiguration> accessTokenConfiguration,
        IOptions<RefreshTokenConfiguration> refreshTokenConfiguration)
    {
        _mapper = mapper;
        _jwtService = jwtService;
        _userRepository = userRepository;
        _cryptographyService = cryptographyService;
        _accessTokenConfiguration = accessTokenConfiguration.Value;
        _refreshTokenConfiguration = refreshTokenConfiguration.Value;
    }

    public async Task<AuthResponse> LoginUser(LoginRequest request)
    {
        var user = await this._userRepository.Get(u => u.Email == request.Email)
            ?? throw new ApiException(message: "User account with this email does not exist.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        if (!this._cryptographyService.Verify(request.Password, user.PasswordHash))
        {
            throw new ApiException(message: "Incorrect email or password provided.", errorCode: AppConstants.ErrorCodeEnum.Unauthorized);
        }

        return new AuthResponse
        {
            TokenContainer = this.GetJwtTokens(user: user),
            User = this._mapper.Map<UserInformation>(user)
        };
    }

    public async Task<AuthResponse> RegisterUser(RegisterRequest request)
    {
        var user = await this._userRepository.Get(u => u.Email == request.Email);
        if (user != null)
        {
            throw new ApiException(message: "User account already exists with this email.", errorCode: AppConstants.ErrorCodeEnum.Conflict);
        }

        var newUser = this._mapper.Map<User>(request);
        newUser.PasswordHash = this._cryptographyService.Hash(request.Password);
        newUser.CreatedBy = newUser.Email;
        newUser.UpdatedBy = newUser.Email;

        await this._userRepository.Add(newUser);

        return new AuthResponse
        {
            TokenContainer = this.GetJwtTokens(user: newUser),
            User = this._mapper.Map<UserInformation>(newUser)
        };
    }

    private JwtTokenContainerModel GetJwtTokens(User user)
    {
        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("email", user.Email),
        };

        return new JwtTokenContainerModel
        {
            AccessToken = this._jwtService.GenerateToken(claims: claims, configuration: this._accessTokenConfiguration),
            RefreshToken = this._jwtService.GenerateToken(claims: claims, configuration: this._refreshTokenConfiguration),
        };
    }
}