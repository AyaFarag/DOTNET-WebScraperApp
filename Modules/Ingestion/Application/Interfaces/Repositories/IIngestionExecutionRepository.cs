using Ingestion.Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IIngestionExecutionRepository
    {
        Task AddAsync(
            IngestionExecution execution,
            CancellationToken cancellationToken);

        Task<IngestionExecution?> GetByBatchIdAsync(
            Guid batchId,
            CancellationToken cancellationToken);

        Task UpdateAsync(
            IngestionExecution execution,
            CancellationToken cancellationToken);
    }
}
