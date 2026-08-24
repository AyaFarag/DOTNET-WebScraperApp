using Hangfire;

namespace Processing.Infrastructure.Presistance.BackgroundJobs
{
    public class OutboxJobRegistration
    {
        public static void Register()
        {
            RecurringJob.AddOrUpdate<ProcessingOutboxPublisherJob>(
            "processing-outbox-publisher",
            job => job.ExecuteAsync(
                CancellationToken.None),
            "*/10 * * * * *");
        }
    }
}
