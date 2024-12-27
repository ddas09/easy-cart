using EasyCart.Shared.DAL.Repositories;
using EasyCart.AuthService.DAL.Entities;
using EasyCart.AuthService.DAL.Contracts;

namespace EasyCart.AuthService.DAL.Repositories;

public class RefreshTokenEntryRepository : CrudBaseRepository<RefreshTokenEntry, AuthDBContext>, IRefreshTokenEntryRepository
{
    public RefreshTokenEntryRepository(AuthDBContext dbContext) : base(dbContext)
    {
    }
}