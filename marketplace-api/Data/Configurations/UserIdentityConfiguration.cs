using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace_api.Data.Configurations;

public class UserIdentityConfiguration : IEntityTypeConfiguration<UserIdentity>
{
    public void Configure(EntityTypeBuilder<UserIdentity> builder)
    {
        builder
            .ToTable("UserIdentities")
            .HasKey(ui => ui.Id);
        builder.Property(ui => ui.UserIdentityId)
            .HasDefaultValueSql("NEWID()");
        builder.Property(ui => ui.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
        builder.Property(ui => ui.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
        builder.Property(ui => ui.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnUpdate();
    }
}