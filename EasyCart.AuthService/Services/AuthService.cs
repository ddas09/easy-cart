using AutoMapper;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Exceptions;
using EasyCart.AuthService.DAL.Entities;
using EasyCart.AuthService.DAL.Contracts;
using EasyCart.AuthService.Models.Request;
using EasyCart.AuthService.Models.Response;
using EasyCart.AuthService.Services.Contracts;

namespace EasyCart.AuthService.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyService _cryptographyService;

    public AuthService(IMapper mapper, IUserRepository userRepository, ICryptographyService cryptographyService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _cryptographyService = cryptographyService;
    }

    public async Task<AuthResponse> LoginUser(LoginRequest request)
    {
        var user = await this._userRepository.Get(u => u.Email == request.Email)
            ?? throw new ApiException(message: "User account with this email does not exist.", errorCode: AppConstants.ErrorCodeEnum.NotFound);

        if (!this._cryptographyService.Verify(request.Password, user.PasswordHash))
        {
            throw new ApiException(message: "Incorrect email or password provided.", errorCode: AppConstants.ErrorCodeEnum.Unauthorized);
        }

        // todo: genrate JWT token for user here
        return new AuthResponse
        {
            Token = string.Empty
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

        // todo: genrate JWT token for user here
        return new AuthResponse
        {
            Token = string.Empty
        };
    }
}