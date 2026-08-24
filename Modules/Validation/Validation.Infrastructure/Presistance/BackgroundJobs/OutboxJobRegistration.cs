using Hangfire;

namespace Validation.Infrastructure.Presistance.BackgroundJobs
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
