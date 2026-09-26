using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Infrastructure.Presistance.Repositories.BackgroundJobs
{
    public class OutboxRepository
    {
        public static void Register()
        {
            RecurringJob.AddOrUpdate<IndexingOutboxPublisherJob>(
            "classification-outbox-publisher",
            job => job.ExecuteAsync(
                CancellationToken.None),
            "*/10 * * * * *");
        }
    }
}
