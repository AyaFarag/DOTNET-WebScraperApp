using Microsoft.EntityFrameworkCore;
using Processing.Application.Interfaces.Repositories;
using Processing.Domain.Entities;
using Processing.Infrastructure.Presistance.Data;

namespace Processing.Infrastructure.Presistance.Repository
{
    public class IdempotencyService : IIdempotencyService
    {
        private readonly ProcessingDbContext _context;
        public IdempotencyService(ProcessingDbContext context)
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
