using marketplace_api.Services.MailService;

namespace marketplace_api.Services;

public class Job
{
    private static int _counter = 0;

    private static IList<string> emails = new List<string>
        { "1@gmail.com", "2@gmail.com", "3@gmail.com", "inalid", "4@gmail.com" };
    public void Execute()
    {
        _counter += 1;
        Console.WriteLine($"Yessss {_counter}");
        foreach (var email in emails)
        {
            new EmailService().Send(email);
        }
        Console.WriteLine($"Nooo {_counter}");
    }
}