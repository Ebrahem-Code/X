using Hangfire;
using X.Application.Core.BackgroundJobs;
using X.Application.Core.Emails;

namespace X.Infrastructure.ExternalServices.BackgroundJobs
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IEmailService _emailService;

        public BackgroundJobService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void ScheduleEmailJob(string to, string subject, string body)
        {
            BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(to, subject, body));
        }
    }
}