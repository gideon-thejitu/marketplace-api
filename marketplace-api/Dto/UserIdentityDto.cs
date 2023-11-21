namespace marketplace_api.Dto;

public class UserIdentityDto
{
    public long Id { get; set; }
    public Guid UserIdentityId { get; set; } = Guid.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}

public class UserIdentityFilterDto : CollectionFilterDto
{}