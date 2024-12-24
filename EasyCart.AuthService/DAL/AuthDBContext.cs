using Microsoft.EntityFrameworkCore;
using EasyCart.AuthService.DAL.Entities;
using EasyCart.AuthService.DAL.Configurations;

namespace EasyCart.AuthService.DAL;

public class AuthDBContext : DbContext
{
    public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}