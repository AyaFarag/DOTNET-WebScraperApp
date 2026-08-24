using Processing.Domain.Entities;

namespace Processing.Application.Interfaces.Repositories
{
    public interface IOutboxRepository
    {
        Task AddAsync(OutboxMessage message, CancellationToken cancellationToken);

        Task<IReadOnlyList<OutboxMessage>> GetPendingAsync(int batchSize, CancellationToken cancellationToken);

        Task MarkAsProcessedAsync(OutboxMessage message, CancellationToken cancellationToken);

        Task MarkAsFailedAsync(OutboxMessage message, string error, CancellationToken cancellationToken);
    }
}
