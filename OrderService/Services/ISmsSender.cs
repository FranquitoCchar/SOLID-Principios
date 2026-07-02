namespace NotificationService.Services;

public interface ISmsSender
{
    Task SendSmsAsync(string message);
}