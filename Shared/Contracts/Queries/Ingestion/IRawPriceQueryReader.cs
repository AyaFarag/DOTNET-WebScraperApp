using Shared.Domain;

namespace Shared.Contracts.Queries.Ingestion
{
    public interface IRawPriceQueryReader<Tsource>
    {
        Task<bool> ExistsAsync(Guid batchId, string productName, string source, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Tsource>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
