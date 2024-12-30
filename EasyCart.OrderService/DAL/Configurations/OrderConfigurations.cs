using EasyCart.OrderService.DAL.Entities;
using EasyCart.Shared.DAL.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCart.OrderService.DAL.Configurations;

internal class OrderConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
    }
}