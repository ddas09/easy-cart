using EasyCart.AuthService.DAL.Entities;
using EasyCart.Shared.DAL.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyCart.AuthService.DAL.Configurations;

internal class RefreshTokenEntryConfiguration : BaseEntityConfiguration<RefreshTokenEntry>
{
    public override void Configure(EntityTypeBuilder<RefreshTokenEntry> builder)
    {
        base.Configure(builder);
    }
}