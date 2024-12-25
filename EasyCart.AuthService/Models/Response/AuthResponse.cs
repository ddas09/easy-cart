namespace EasyCart.AuthService.Models.Response;

public class AuthResponse
{
    public string Token { get; set; }

    public UserInformation User { get; set; }
}

public class UserInformation
{
    public int Id { get; set; }

    public string Email { get; set; }
}