namespace X.Application.Core.Notifications;

public interface INotificationService
{
    Task SendNotificationAsync(string title, string body, string token);
}