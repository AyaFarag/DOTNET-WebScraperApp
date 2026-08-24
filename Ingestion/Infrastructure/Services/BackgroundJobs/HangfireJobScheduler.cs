using Application.Interfaces;
using Hangfire;


namespace Infrastructure.Services.BackgroundJobs
{
    public sealed class HangfireJobScheduler : IJobScheduler
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireJobScheduler(
            IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public string EnqueueScraping()
        {
            var result= _backgroundJobClient.Enqueue<ScrapingJob>(
                job => job.ExecuteAsync(
                    CancellationToken.None));
            return result;
        }

        public string ScheduleScraping(
            DateTimeOffset executeAt)
        {
            return _backgroundJobClient.Schedule<ScrapingJob>(
                job => job.ExecuteAsync(
                    CancellationToken.None),
                executeAt);
        }

        public void ScheduleRecurringScraping(
            string jobId,
            string cronExpression)
        {
            RecurringJob.AddOrUpdate<ScrapingJob>(
                jobId,
                job => job.ExecuteAsync(
                    CancellationToken.None),
                cronExpression);
        }
    }
 }
