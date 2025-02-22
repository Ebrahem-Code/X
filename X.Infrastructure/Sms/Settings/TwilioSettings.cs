namespace X.Infrastructure.Sms.Settings;

public class TwilioSettings
{
    public string AccountSid { get; set; } = default!;
    public string AuthToken { get; set; } = default!;
    public string FromPhoneNumber { get; set; } = default!;
}
