namespace X.Application.Core.BackgroundJobs
{
    public interface IBackgroundJobService
    {
        void ScheduleEmailJob(string to, string subject, string body);
    }
}