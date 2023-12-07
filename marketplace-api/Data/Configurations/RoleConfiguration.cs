using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace_api.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(role => role.Id);
        builder.HasMany<UserIdentityRole>(role => role.UserIdentityRoles)
            .WithOne(userIdentityRole => userIdentityRole.Role)
            .HasForeignKey(userIdentityRole => userIdentityRole.RoleId);
    }
}