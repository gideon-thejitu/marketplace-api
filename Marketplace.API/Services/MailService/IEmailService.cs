namespace Marketplace.Services.Mail;

public interface IEmailService
{
    public Task Send(string to);
}