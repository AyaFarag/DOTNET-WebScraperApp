using Shared.Domain;

namespace Shared.Contracts.Queries.Ingestion
{
    public interface IRawPriceQueryReader
    {
        Task<bool> ExistsAsync(Guid batchId, string productName, string source, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<RawPrice>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
