using Microsoft.EntityFrameworkCore;
using EasyCart.ProductService.DAL.Entities;
using EasyCart.ProductService.DAL.Configurations;

namespace EasyCart.ProductService.DAL;

public class ProductDBContext : DbContext
{
    public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}