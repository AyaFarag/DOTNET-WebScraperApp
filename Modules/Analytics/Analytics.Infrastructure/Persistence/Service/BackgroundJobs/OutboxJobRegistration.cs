using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Infrastructure.Persistence.Service.BackgroundJobs
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
