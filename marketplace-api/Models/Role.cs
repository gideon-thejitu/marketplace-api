using marketplace_api.Data.Configurations;

namespace marketplace_api.Models;

public class Role
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = String.Empty;

    public ICollection<UserIdentityRole> UserIdentityRoles { get; } = new List<UserIdentityRole>();
}
