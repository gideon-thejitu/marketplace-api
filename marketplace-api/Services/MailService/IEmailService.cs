namespace marketplace_api.Services.MailService;

public interface IEmailService
{
    public Task Send(string to);
}