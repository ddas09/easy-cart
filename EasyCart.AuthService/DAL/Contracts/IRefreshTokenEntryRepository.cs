using EasyCart.Shared.DAL.Contracts;
using EasyCart.AuthService.DAL.Entities;

namespace EasyCart.AuthService.DAL.Contracts;

public interface IRefreshTokenEntryRepository : ICrudBaseRepository<RefreshTokenEntry>
{
}