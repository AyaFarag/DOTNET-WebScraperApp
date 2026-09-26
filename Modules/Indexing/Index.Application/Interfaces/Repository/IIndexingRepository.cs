using Index.Domain.Entities;

namespace Index.Application.Interfaces.Repository
{
    public interface IIndexingRepository
    {
        Task AddAsync(IndexedProduct entity, CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<IndexedProduct> entities, CancellationToken cancellationToken);

        Task<IndexedProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<IndexedProduct>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid classificationResultId, CancellationToken cancellationToken);
    }
}
