namespace X.Application.Core.Emails;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
