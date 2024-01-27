namespace Marketplace.Helpers;

public class BCryptHelper : IBcryptHelper
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string rawPassword, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(rawPassword, hash);
    }
}
