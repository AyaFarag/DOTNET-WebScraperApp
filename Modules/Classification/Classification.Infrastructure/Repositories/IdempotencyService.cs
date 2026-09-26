using Classification.Application.Interfaces;
using Classification.Domain.Enttities;
using Classification.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Classification.Infrastructure.Repositories
{
    public class IdempotencyService : IIdempotencyService
    {
        private readonly ClassificationDbContext _context;
        public IdempotencyService(ClassificationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsProcessedAsync(Guid eventId, CancellationToken cancellationToken)
        {
            return await _context.ProcessedEvents.AnyAsync(x => x.EventId == eventId, cancellationToken);
        }

        public async Task MarkAsProcessedAsync(Guid eventId, string eventType, CancellationToken cancellationToken)
        {
            var processedEvent = new ProcessedEvent(eventId, eventType);

            await _context.ProcessedEvents.AddAsync(processedEvent, cancellationToken);
        }
    }
}
