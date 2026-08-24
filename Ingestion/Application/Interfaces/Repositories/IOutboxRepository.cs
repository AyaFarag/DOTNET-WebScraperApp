using Ingestion.Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IOutboxRepository
    {
        Task AddAsync(
            Guid eventId, 
            Guid batchId, 
            CancellationToken cancellationToken);
        Task<List<OutboxMessage>> GetPendingAsync(
            int batchSize, 
            CancellationToken cancellationToken);
        Task MarkAsProcessedAsync(
            OutboxMessage message, 
            CancellationToken cancellationToken);
        Task MarkAsFailedAsync(
            OutboxMessage message, 
            string error, 
            CancellationToken cancellationToken);
    }
}
