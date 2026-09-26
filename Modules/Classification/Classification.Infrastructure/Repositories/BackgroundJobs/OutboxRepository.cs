using Hangfire;

namespace Classification.Infrastructure.Repositories.BackgroundJobs
{
    public class OutboxRepository
    {
        public static void Register()
        {
            RecurringJob.AddOrUpdate<ClassificationOutboxPublisherJob>(
            "classification-outbox-publisher",
            job => job.ExecuteAsync(
                CancellationToken.None),
            "*/10 * * * * *");
        }
    }
}
