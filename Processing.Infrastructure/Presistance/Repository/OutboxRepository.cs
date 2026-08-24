using Microsoft.EntityFrameworkCore;
using Processing.Application.Interfaces.Repositories;
using Processing.Domain.Entities;
using Processing.Infrastructure.Presistance.Data;

namespace Processing.Infrastructure.Presistance.Repository
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly ProcessingDbContext _context;
        public OutboxRepository(ProcessingDbContext context)
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
            message.MarkAsFailed(error);

            return Task.CompletedTask;
        }
    }
}
