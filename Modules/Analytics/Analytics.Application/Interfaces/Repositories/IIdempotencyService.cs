using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Interfaces.Repositories
{
    public interface IIdempotencyService
    {
        Task<bool> IsProcessedAsync(Guid eventId, CancellationToken cancellationToken);

        Task MarkAsProcessedAsync(Guid eventId, string eventType, CancellationToken cancellationToken);
    }
}
