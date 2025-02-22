namespace X.Application.Core.Emails;

public interface ISmsService
{
    Task SendSmsAsync(string phoneNumber, string message);
}
