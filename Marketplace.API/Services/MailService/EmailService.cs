using System.Net;
using System.Net.Mail;
using Marketplace.Exceptions;

namespace Marketplace.Services.Mail;

public class EmailService : IEmailService
{
    private readonly MailMessage _email;
    private readonly SmtpClient _client;

    public EmailService()
    {
        _email = new MailMessage();
        _client = new SmtpClient("localhost");
        _email.From = new MailAddress("marketplace.dev@mail.com", "Market Place Dev");
        _client.EnableSsl = false;
        _client.Port = 1025;
        _client.Credentials = new NetworkCredential("", "");
    }

    public async Task Send(string to)
    {
        try
        {
            Console.WriteLine($"Processing {to}");
            _email.To.Add(to);
            _email.Body = $"<h1>Hello {to}</h1>";
            _email.Subject = "Market Place Dev";
            _email.IsBodyHtml = true;
            _client.Send(_email);
            _email.To.Clear();
        }
        catch (Exception e)
        {
            throw new EmailNotSentException($"Email '{to}' could not be sent", e);
        }
    }
}