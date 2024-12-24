using EasyCart.Shared.DAL.Repositories;
using EasyCart.AuthService.DAL.Entities;
using EasyCart.AuthService.DAL.Contracts;

namespace EasyCart.AuthService.DAL.Repositories;

public class UserRepository : CrudBaseRepository<User, AuthDBContext>, IUserRepository
{
    public UserRepository(AuthDBContext dbContext) : base(dbContext)
    {
    }
}