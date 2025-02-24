using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using X.Application.Core.Notifications;

namespace X.Infrastructure.ExternalServices.Notifications
{
    internal class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IConfiguration configuration, ILogger<NotificationService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            var firebaseCredentialPath = _configuration["Firebase:CredentialPath"];
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(firebaseCredentialPath)
            });
        }

        public async Task SendNotificationAsync(string title, string body, string token)
        {
            var message = new Message
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Token = token
            };

            try
            {
                var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                _logger.LogInformation("Notification sent successfully: {Response}", response);
            }
            catch (FirebaseMessagingException ex)
            {
                _logger.LogError(ex, "Error sending notification");
                throw;
            }
        }
    }
}