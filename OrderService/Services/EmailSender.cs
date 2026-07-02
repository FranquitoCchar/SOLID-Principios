namespace NotificationService.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string message)
    {
        Console.WriteLine(message);

        return Task.CompletedTask;
    }
}