using Analytics.Application.Interfaces.AI.Repository;
using Analytics.Application.Interfaces.AI.Service;
using Analytics.Application.Interfaces.Repositories;

namespace Analytics.Application.Services.AI
{
    public sealed class PriceSnapshotSearchRebuildService : IPriceSnapshotSearchRebuildService
    {
        private readonly IPriceSnapshotRepository _repository;
        private readonly IPriceSnapshotSearchIndexer _indexer;

        public PriceSnapshotSearchRebuildService(
            IPriceSnapshotRepository repository,
            IPriceSnapshotSearchIndexer indexer)
        {
            _repository = repository;
            _indexer = indexer;
        }

        public async Task RebuildAsync(
            CancellationToken cancellationToken)
        {
            // Initial/simple implementation.
            // Later this should be paged/batched.
            var snapshots = await _repository.GetAllAsync(cancellationToken);

            await _indexer.IndexBatchAsync(snapshots, cancellationToken);
        }
    }
}
