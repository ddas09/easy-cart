using EasyCart.Shared.DAL.Entities;

namespace EasyCart.AuthService.DAL.Entities;

public class RefreshTokenEntry : BaseEntity
{    
    public int UserId { get; set; }

    public User User { get; set; }

    public string Token { get; set; }
}