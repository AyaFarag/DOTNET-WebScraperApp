using Microsoft.EntityFrameworkCore;
using Validation.Application.Interfaces;
using Validation.Domain.Entities;
using Validation.Infrastructure.Presistance.Data;

namespace Validation.Infrastructure.Presistance.Repositories
{
    public sealed class IdempotencyService: IIdempotencyService
    {
        private readonly ValidationDbContext _context;

        public IdempotencyService(ValidationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsProcessedAsync(Guid eventId,CancellationToken cancellationToken)
        {
            return await _context.ProcessedEvents.AnyAsync( x => x.EventId == eventId,cancellationToken);
        }

        public async Task MarkAsProcessedAsync(Guid eventId,string eventType, CancellationToken cancellationToken)
        {
            var processedEvent = new ProcessedEvent(eventId,eventType);

            await _context.ProcessedEvents.AddAsync(processedEvent,cancellationToken);
        }
    }
}
