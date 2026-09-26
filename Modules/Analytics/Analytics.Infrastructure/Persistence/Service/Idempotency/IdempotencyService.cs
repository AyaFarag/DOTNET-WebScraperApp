using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Infrastructure.Persistence.Service.Idempotency
{
    public class IdempotencyService : IIdempotencyService
    {
        private readonly AnalyticsDbContext _context;
        public IdempotencyService(AnalyticsDbContext context)
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
