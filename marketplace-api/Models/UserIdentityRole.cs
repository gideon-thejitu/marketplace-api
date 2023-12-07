using marketplace_api.Models;

namespace marketplace_api.Data.Configurations;

public class UserIdentityRole
{
    public long RoleId { get; set; }
    public long UserIdentityId { get; set; }

    public Role Role { get; set; }
    public UserIdentity UserIdentity { get; set; }
}
