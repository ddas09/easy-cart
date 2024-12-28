using EasyCart.CartService.DAL.Entities;
using EasyCart.Shared.DAL.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCart.CartService.DAL.Configurations;

internal class CartConfiguration : BaseEntityConfiguration<Cart>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        base.Configure(builder);

        // one user can only have one cart
        builder.HasIndex(cart => cart.UserId)
            .IsUnique();
    }
}