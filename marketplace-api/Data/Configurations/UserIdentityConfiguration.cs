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
            .HasKey(userIdentity => userIdentity.Id);
        builder.Property(userIdentity => userIdentity.UserIdentityId)
            .HasDefaultValueSql("NEWID()");
        builder.Property(userIdentity => userIdentity.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
        builder.Property(userIdentity => userIdentity.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
        builder.Property(userIdentity => userIdentity.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnUpdate();
        builder.HasMany<UserIdentityRole>(userIdentity => userIdentity.UserIdentityRoles)
            .WithOne(userIdentityRole => userIdentityRole.UserIdentity)
            .HasForeignKey(userIdentityRole => userIdentityRole.UserIdentityId);
    }
}