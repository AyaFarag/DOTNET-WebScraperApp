using Classification.Domain.Enttities;

namespace Classification.Application.Interfaces.Repository
{
    public interface IClassificationRepository
    {
        Task AddAsync(ClassificationResult entity,CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<ClassificationResult> entities, CancellationToken cancellationToken);

        Task<ClassificationResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<ClassificationResult>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
