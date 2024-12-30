using Microsoft.EntityFrameworkCore;
using EasyCart.OrderService.DAL.Entities;
using EasyCart.OrderService.DAL.Configurations;

namespace EasyCart.OrderService.DAL;

public class OrderDBContext : DbContext
{
    public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OrderConfiguration());

        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
    }
}