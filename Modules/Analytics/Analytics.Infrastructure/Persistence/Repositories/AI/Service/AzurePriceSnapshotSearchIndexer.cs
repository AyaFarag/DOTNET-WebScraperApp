using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.Repository;
using Analytics.Domain.Entities;
using Azure.Search.Documents;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Service
{
    public sealed class AzurePriceSnapshotSearchIndexer : IPriceSnapshotSearchIndexer
    {
        private readonly SearchClient _searchClient;

        public AzurePriceSnapshotSearchIndexer(SearchClient searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task IndexAsync(PriceSnapshot snapshot,CancellationToken cancellationToken)
        {
            var document = MapToDocument(snapshot);

            await _searchClient.MergeOrUploadDocumentsAsync(new[] { document }, cancellationToken: cancellationToken);
        }

        public async Task IndexBatchAsync(IReadOnlyCollection<PriceSnapshot> snapshots, CancellationToken cancellationToken)
        {
            if (snapshots.Count == 0)
                return;

            var documents = snapshots.Select(MapToDocument).ToList();

            await _searchClient.MergeOrUploadDocumentsAsync(documents,cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid snapshotId,CancellationToken cancellationToken)
        {
            await _searchClient.DeleteDocumentsAsync(nameof(PriceSnapshotSearchDocument.Id),
                new[]
                {
                snapshotId.ToString()
                },
                cancellationToken: cancellationToken);
        }

        private static PriceSnapshotSearchDocument MapToDocument(PriceSnapshot snapshot)
        {
            return new PriceSnapshotSearchDocument
            {
                Id = snapshot.Id.ToString(),

                ProductKey = snapshot.ProductKey,

                ProductName = snapshot.ProductName,

                Brand = snapshot.Brand,

                CategoryCode = snapshot.CategoryCode,

                Source = snapshot.Source,

                Price = snapshot.Price,

                UnitPrice = snapshot.UnitPrice,

                Currency = snapshot.Currency,

                CollectedAt = snapshot.CollectedAt
            };
        }
    }
}
