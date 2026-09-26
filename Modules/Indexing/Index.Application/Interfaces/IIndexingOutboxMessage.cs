using Index.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.Interfaces
{
    public interface IIndexingOutboxMessage
    {
        Task AddAsync(OutboxMessage message, CancellationToken cancellationToken);

        Task<IReadOnlyList<OutboxMessage>> GetPendingAsync(int batchSize, CancellationToken cancellationToken);

        Task MarkAsProcessedAsync(OutboxMessage message, CancellationToken cancellationToken);

        Task MarkAsFailedAsync(OutboxMessage message, string error, CancellationToken cancellationToken);
    }
}
