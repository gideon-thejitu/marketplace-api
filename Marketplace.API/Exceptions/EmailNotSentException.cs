namespace Marketplace.Exceptions;

public class EmailNotSentException : MarketplaceException
{
    public EmailNotSentException() : base()
    {}
    
    public EmailNotSentException(string message) : base(message)
    {}
    
    public EmailNotSentException(string message, Exception inner) : base(message, inner)
    {}

}
