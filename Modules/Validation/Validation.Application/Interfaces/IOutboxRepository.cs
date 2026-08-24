
using Validation.Domain.Entities;

namespace Validation.Application.Interfaces
{
    public interface IOutboxRepository
    {
        Task AddAsync(OutboxMessage message, CancellationToken cancellationToken);

        Task<IReadOnlyList<OutboxMessage>> GetPendingAsync(int batchSize, CancellationToken cancellationToken);

        Task MarkAsProcessedAsync(OutboxMessage message, CancellationToken cancellationToken);

        Task MarkAsFailedAsync(OutboxMessage message,string error, CancellationToken cancellationToken);
    }
}
