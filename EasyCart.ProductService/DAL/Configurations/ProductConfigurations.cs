using Microsoft.EntityFrameworkCore;
using EasyCart.Shared.DAL.Configurations;
using EasyCart.ProductService.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCart.ProductService.DAL.Configurations;

internal class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Stock)
            .HasDefaultValue(1);

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);
    }
}