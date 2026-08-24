using Microsoft.EntityFrameworkCore;
using Validation.Application.Interfaces;
using Validation.Domain.Entities;
using Validation.Infrastructure.Presistance.Data;

namespace Validation.Infrastructure.Presistance.Repositories
{
    public sealed class OutboxRepository : IOutboxRepository
    {
        private readonly ValidationDbContext _context;

        public OutboxRepository(ValidationDbContext context)
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

        public Task MarkAsFailedAsync(OutboxMessage message, string error,CancellationToken cancellationToken)
        {
            message.MarkFailed(error);

            return Task.CompletedTask;
        }
    }
}
