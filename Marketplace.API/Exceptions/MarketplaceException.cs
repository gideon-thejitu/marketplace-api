namespace Marketplace.Exceptions;

public class MarketplaceException : Exception
{
    public MarketplaceException() : base()
    {}

    public MarketplaceException(string message) : base(message)
    {}

    public MarketplaceException(string message, Exception inner) : base(message, inner)
    {}
}
