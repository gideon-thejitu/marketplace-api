using System.Security.Policy;

namespace marketplace_api.Helpers;

public interface IBcryptHelper
{
    public string Hash(string password);
}
