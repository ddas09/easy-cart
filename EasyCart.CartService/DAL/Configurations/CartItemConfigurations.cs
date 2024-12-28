using Microsoft.EntityFrameworkCore;
using EasyCart.CartService.DAL.Entities;
using EasyCart.Shared.DAL.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCart.CartService.DAL.Configurations;

internal class CartItemConfiguration : BaseEntityConfiguration<CartItem>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Quantity)
            .HasDefaultValue(1);

        // One user cart can't have multiple items of same product
        builder.HasIndex(c => new { c.CartId, c.ProductId })
            .IsUnique();
    }
}