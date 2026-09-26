using Index.Application.Interfaces;
using Index.Domain.Entities;
using Index.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Index.Infrastructure.Presistance.Repositories
{
    public class IndexingOutboxMessagesRepository : IIndexingOutboxMessage
    {
        private readonly IndexingDbContext _context;
        public IndexingOutboxMessagesRepository(IndexingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxMessage message, CancellationToken cancellationToken)
        {
            await _context.outboxMessages.AddAsync(message, cancellationToken);
        }

        public async Task<IReadOnlyList<OutboxMessage>> GetPendingAsync(int batchSize, CancellationToken cancellationToken)
        {
            return await _context.outboxMessages
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
            message.MarkAsFailed(error); // edit it

            return Task.CompletedTask;
        }
    }
}
