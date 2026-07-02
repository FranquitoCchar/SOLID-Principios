namespace NotificationService.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string message);
}