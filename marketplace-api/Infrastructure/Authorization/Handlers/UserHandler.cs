using marketplace_api.Models;

namespace marketplace_api.Infrastructure.Authorization.Handlers;

public class UserHandler : IBaseResourceHandler
{
    public void Execute()
    {
        
    }

    private IDictionary<string, string> BuildProperties(UserIdentity user)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        return result;
    }
}