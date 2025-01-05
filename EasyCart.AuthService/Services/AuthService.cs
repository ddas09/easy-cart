using AutoMapper;
using System.Transactions;
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
    private readonly IRefreshTokenEntryRepository _refreshTokenEntryRepository;

    public AuthService
    (
        IMapper mapper, 
        IJwtService jwtService,
        IUserRepository userRepository, 
        ICryptographyService cryptographyService,
        IRefreshTokenEntryRepository refreshTokenEntryRepository,
        IOptions<AccessTokenConfiguration> accessTokenConfiguration,
        IOptions<RefreshTokenConfiguration> refreshTokenConfiguration)
    {
        _mapper = mapper;
        _jwtService = jwtService;
        _userRepository = userRepository;
        _cryptographyService = cryptographyService;
        _refreshTokenEntryRepository = refreshTokenEntryRepository;
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
            TokenContainer = await this.GetJwtTokens(user: user),
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
            TokenContainer = await this.GetJwtTokens(user: newUser),
            User = this._mapper.Map<UserInformation>(newUser)
        };
    }

    public async Task<JwtTokenContainerModel> RefreshToken(RefreshTokenRequestModel request)
    {
        bool isValidToken = this._jwtService.IsValidToken(configuration: this._refreshTokenConfiguration, token: request.RefreshToken);
        if (!isValidToken)
        {
            throw new ApiException(message: "Invalid refresh token provided.", errorCode: AppConstants.ErrorCodeEnum.InvalidRefreshToken);
        }

        var refrehTokenEntry = await this._refreshTokenEntryRepository.Get(rt => rt.Token == request.RefreshToken) 
            ?? throw new ApiException(message: "Refresh token has been expired.", errorCode: AppConstants.ErrorCodeEnum.InvalidRefreshToken);

        // This scenario should never occur practically, as this entry we are getting from SQL DB
        var user = await this._userRepository.Get(u => u.Id == refrehTokenEntry.UserId) 
            ?? throw new ApiException(message: "Invalid user token; user doesn't exist.", errorCode: AppConstants.ErrorCodeEnum.InvalidRefreshToken);

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        // invalidate previous tokens
        var refreshTokenEntries = await this.GetRefreshTokensByUserId(refrehTokenEntry.UserId);
        await this._refreshTokenEntryRepository.DeleteRange(refreshTokenEntries);

        var tokenContainer = await this.GetJwtTokens(user: user);

        var newRefreshTokenEntry = new RefreshTokenEntry()
        {
            Token = tokenContainer.RefreshToken,
            UserId = refrehTokenEntry.UserId,
        };
        await this._refreshTokenEntryRepository.Add(newRefreshTokenEntry);

        scope.Complete();

        return tokenContainer;
    }

    public async Task Logout(int userId)
    {
        var user = await this._userRepository.Get(u => u.Id == userId) 
            ?? throw new ApiException(message: "User doesn't exist.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        var refreshTokenEntries = await this.GetRefreshTokensByUserId(userId);
        await this._refreshTokenEntryRepository.DeleteRange(refreshTokenEntries);
    }

    private async Task<IEnumerable<RefreshTokenEntry>> GetRefreshTokensByUserId(int userId)
    {
        return await _refreshTokenEntryRepository.GetList(predicate: rt => rt.UserId == userId);
    }

    private async Task<JwtTokenContainerModel> GetJwtTokens(User user)
    {
        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("email", user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var accessToken = this._jwtService.GenerateToken(claims: claims, configuration: this._accessTokenConfiguration);
        var refreshToken = this._jwtService.GenerateToken(claims: claims, configuration: this._refreshTokenConfiguration);

        var refreshTokenEntry = new RefreshTokenEntry
        {
            Token = refreshToken,
            UserId = user.Id,
            CreatedBy = user.Email,
            UpdatedBy = user.Email
        };

        await this._refreshTokenEntryRepository.Add(refreshTokenEntry);

        return new JwtTokenContainerModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}