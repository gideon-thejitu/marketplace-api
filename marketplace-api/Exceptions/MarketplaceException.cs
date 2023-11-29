namespace marketplace_api.Exceptions;

public class MarketplaceException : Exception
{
    protected MarketplaceException() : base()
    {}

    protected MarketplaceException(string message) : base(message)
    {}

    protected MarketplaceException(string message, Exception inner) : base(message, inner)
    {}
}
