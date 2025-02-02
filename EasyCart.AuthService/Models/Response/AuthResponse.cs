namespace EasyCart.AuthService.Models.Response;

public class AuthResponse
{
    public UserInformation User { get; set; }

    public JwtTokenContainerModel TokenContainer { get; set; }
}

public class UserInformation
{
    public int Id { get; set; }

    public string Email { get; set; }

    public bool IsAdmin { get; set; }
}