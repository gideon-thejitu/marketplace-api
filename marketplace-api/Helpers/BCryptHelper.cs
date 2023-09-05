namespace marketplace_api.Helpers;

public class BCryptHelper : IBcryptHelper
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
