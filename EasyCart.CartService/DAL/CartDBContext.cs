using Microsoft.EntityFrameworkCore;
using EasyCart.CartService.DAL.Entities;
using EasyCart.CartService.DAL.Configurations;

namespace EasyCart.CartService.DAL;

public class CartDBContext : DbContext
{
    public CartDBContext(DbContextOptions<CartDBContext> options) : base(options) { }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CartConfiguration());

        modelBuilder.ApplyConfiguration(new CartItemConfiguration());
    }
}