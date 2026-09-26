using Index.Application.Interfaces;
using Index.Domain.Entities;
using Index.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Index.Infrastructure.Presistance.Repositories
{
    public class IdempotencyService : IIdempotencyService
    {
        private readonly IndexingDbContext _context;
        public IdempotencyService(IndexingDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsProcessedAsync(Guid eventId, CancellationToken cancellationToken)
        {
            return await _context.processedEvents.AnyAsync(x => x.EventId == eventId, cancellationToken);
        }

        public async Task MarkAsProcessedAsync(Guid eventId, string eventType, CancellationToken cancellationToken)
        {
            var processedEvent = new ProcessedEvent(eventId, eventType);

            await _context.processedEvents.AddAsync(processedEvent, cancellationToken);
        }
    }
}
