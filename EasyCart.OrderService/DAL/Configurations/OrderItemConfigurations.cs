using Microsoft.EntityFrameworkCore;
using EasyCart.OrderService.DAL.Entities;
using EasyCart.Shared.DAL.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCart.OrderService.DAL.Configurations;

internal class OrderItemConfiguration : BaseEntityConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Quantity)
            .HasDefaultValue(1);

        // Same order can't have multiple items of same product
        builder.HasIndex(c => new { c.OrderId, c.ProductId })
            .IsUnique();
    }
}