using System.Security.Policy;

namespace Marketplace.Helpers;

public interface IBcryptHelper
{
    public string Hash(string password);
    public bool Verify(string rawPassword, string hash);
}
