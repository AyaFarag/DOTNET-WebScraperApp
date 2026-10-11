using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.AI.PriceSnapshotSearch
{
    public interface IPriceSnapshotSearchIndexer
    {
        Task IndexAsync(
            PriceSnapshot snapshot,
            CancellationToken cancellationToken);

        Task IndexBatchAsync(
            IReadOnlyCollection<PriceSnapshot> snapshots,
            CancellationToken cancellationToken);

        Task DeleteAsync(
            Guid snapshotId,
            CancellationToken cancellationToken);
    }
}
