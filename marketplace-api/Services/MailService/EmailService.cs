using System.Net;
using System.Net.Mail;
using marketplace_api.Exceptions;

namespace marketplace_api.Services.MailService;

public class EmailService : IEmailService
{
    private readonly MailMessage _email;
    private readonly SmtpClient _client;

    public EmailService()
    {
        _email = new MailMessage();
        _client = new SmtpClient("smtp.ethereal.email");
        _email.From = new MailAddress("marketplace.dev@mail.com", "Market Place Dev");
        _client.EnableSsl = true;
        _client.Port = 587;
        _client.Credentials = new NetworkCredential("elwyn81@ethereal.email", "4zqUPzBHknGff2GuAy");
    }

    public void Send(string to)
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
            throw new EmailNotSentException("Email could not be sent", e);
        }
    }
}