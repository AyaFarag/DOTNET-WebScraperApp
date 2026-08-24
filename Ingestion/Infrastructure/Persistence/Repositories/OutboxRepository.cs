using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Data;
using Ingestion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Events.Ingestion;
using System.Text.Json;

namespace Infrastructure.Persistence.Repositories
{
    public sealed class OutboxRepository : IOutboxRepository
    {
        private readonly IngestionDbContext _context;

        public OutboxRepository(IngestionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Guid eventId , Guid batchId , CancellationToken cancellationToken)
        {
            var @event = new PriceDataCollectedEvent(eventId, batchId);

            var payload = JsonSerializer.Serialize(@event);

            var outboxMessage = new OutboxMessage(eventId, typeof(PriceDataCollectedEvent).AssemblyQualifiedName!, payload);

            await _context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
        }

        public async Task<List<OutboxMessage>> GetPendingAsync(int batchSize,CancellationToken cancellationToken)
        {
            return await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null)
                .OrderBy(x => x.OccurredOnUtc)
                .Take(batchSize)
                .ToListAsync(cancellationToken);
        }

        public Task MarkAsProcessedAsync(OutboxMessage message,CancellationToken cancellationToken)
        {
            message.MarkProcessed();

            return Task.CompletedTask;
        }

        public Task MarkAsFailedAsync(OutboxMessage message,string error,CancellationToken cancellationToken)
        {
            message.MarkFailed(error);

            return Task.CompletedTask;
        }
    }
}
