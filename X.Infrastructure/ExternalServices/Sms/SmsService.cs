using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using X.Application.Core.Emails;
using X.Infrastructure.ExternalServices.Sms.Settings;

namespace X.Infrastructure.ExternalServices.Sms;

internal sealed class TwilioSmsService : ISmsService
{
    private readonly TwilioSettings _twilioSettings;
    private readonly ILogger<TwilioSmsService> _logger;

    public TwilioSmsService(IOptions<TwilioSettings> twilioSettings, ILogger<TwilioSmsService> logger)
    {
        _twilioSettings = twilioSettings.Value;
        _logger = logger;
    }

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

        try
        {
            var messageResource = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(_twilioSettings.FromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

            _logger.LogInformation("SMS sent to {PhoneNumber}. SID: {Sid}", phoneNumber, messageResource.Sid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending SMS to {PhoneNumber}", phoneNumber);
            throw;
        }
    }
}
