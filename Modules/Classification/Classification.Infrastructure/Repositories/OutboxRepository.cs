using Classification.Application.Interfaces;
using Classification.Domain.Enttities;
using Classification.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Classification.Infrastructure.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly ClassificationDbContext _context;
        public OutboxRepository(ClassificationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxMessage message, CancellationToken cancellationToken)
        {
            await _context.OutboxMessages.AddAsync(message, cancellationToken);
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
            message.MarkAsProcessed();

            return Task.CompletedTask;
        }

        public Task MarkAsFailedAsync(OutboxMessage message, string error, CancellationToken cancellationToken)
        {
            message.MarkAsFailed(error, DateTime.UtcNow.AddMicroseconds(2)); // edit it

            return Task.CompletedTask;
        }
    }
}
