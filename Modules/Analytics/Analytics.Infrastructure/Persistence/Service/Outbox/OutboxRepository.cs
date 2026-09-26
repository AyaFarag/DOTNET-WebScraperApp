using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Service.Outbox
{
    public sealed class OutboxRepository : IOutboxRepository
    {
        private readonly AnalyticsDbContext _context;

        public OutboxRepository(AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxMessage message, CancellationToken cancellationToken)
        {
            await _context.OutboxMessages.AddAsync(
                message,
                cancellationToken);
        }

        public async Task<IReadOnlyList<OutboxMessage>> GetPendingAsync(int batchSize, CancellationToken cancellationToken)
        {
            return await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null)
                .OrderBy(x => x.OccurredOnUtc)
                .Take(batchSize)
                .ToListAsync(cancellationToken);
        }

        public Task MarkAsProcessedAsync(OutboxMessage message, CancellationToken cancellationToken)
        {
            message.MarkProcessed();

            return Task.CompletedTask;
        }

        public Task MarkAsFailedAsync(OutboxMessage message, string error, CancellationToken cancellationToken)
        {
            message.MarkFailed(error);

            return Task.CompletedTask;
        }
    }
}
