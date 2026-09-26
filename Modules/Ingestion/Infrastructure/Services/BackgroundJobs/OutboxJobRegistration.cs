using Hangfire;

namespace Infrastructure.Services.BackgroundJobs
{
    public static class OutboxJobRegistration
    {
        public static void Register()
        {
            RecurringJob.AddOrUpdate<OutboxPublisherJob>(
                "outbox-publisher",
                job => job.ExecuteAsync(
                    CancellationToken.None),
                "*/10 * * * * *");
        }
    }
}
