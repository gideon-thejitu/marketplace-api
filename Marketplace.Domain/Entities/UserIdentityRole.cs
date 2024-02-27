using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Data.Configurations;

public class UserIdentityRole
{
    public long RoleId { get; set; }
    public long UserIdentityId { get; set; }

    public Role Role { get; set; }
    public UserIdentity UserIdentity { get; set; }
}
