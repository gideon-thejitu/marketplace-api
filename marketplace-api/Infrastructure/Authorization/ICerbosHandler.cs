namespace marketplace_api.Infrastructure.Authorization;

public interface ICerbosHandler
{
    public Task<bool> Handle(string email, string resource, params string[] actions); 
}