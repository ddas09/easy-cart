using EasyCart.Shared.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace EasyCart.AuthService.DAL.Entities;

public class User : BaseEntity
{
    [MaxLength(254)]
    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public string Role { get; set; } = "User"; // Default role
}