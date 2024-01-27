using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Domain.Data.Configurations;

public class UserIdentityRoleConfiguration :  IEntityTypeConfiguration<UserIdentityRole>
{
    public void Configure(EntityTypeBuilder<UserIdentityRole> builder)
    {
        builder.ToTable("UserIdentitiesRoles");
        builder.HasKey(userIdentityRole => new
        {
            userIdentityRole.UserIdentityId,
            userIdentityRole.RoleId
        });
        builder.HasOne<UserIdentity>(userIdentityRole => userIdentityRole.UserIdentity)
            .WithMany(userIdentity => userIdentity.UserIdentityRoles)
            .HasForeignKey(userIdentityRole => userIdentityRole.UserIdentityId);
        builder.HasOne<Role>(userIdentityRole => userIdentityRole.Role)
            .WithMany(role => role.UserIdentityRoles)
            .HasForeignKey(userIdentityRole => userIdentityRole.RoleId);
    }
}