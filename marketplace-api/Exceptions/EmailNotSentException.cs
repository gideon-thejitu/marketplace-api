namespace marketplace_api.Exceptions;

public class EmailNotSentException : Exception
{
    public EmailNotSentException()
    {}
    
    public EmailNotSentException(string message) : base(message)
    {}
    
    public EmailNotSentException(string message, Exception inner) : base(message)
    {}
}
