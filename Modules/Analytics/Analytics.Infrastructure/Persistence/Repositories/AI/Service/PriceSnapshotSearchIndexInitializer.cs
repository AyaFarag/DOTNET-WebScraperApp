using Analytics.Application.Interfaces.AI.PriceSnapshotSearch;
using Analytics.Infrastructure.Configurations;
using Azure.Search.Documents.Indexes;
using Microsoft.Extensions.Options;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Service
{
    public sealed class PriceSnapshotSearchIndexInitializer : IPriceSnapshotSearchIndexInitializer
    {
        private readonly SearchIndexClient _indexClient;
        private readonly AzureSearchOptions _options;

        public PriceSnapshotSearchIndexInitializer(SearchIndexClient indexClient, IOptions<AzureSearchOptions> options)
        {
            _indexClient = indexClient;
            _options = options.Value;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            var index = PriceSnapshotSearchIndex.Create(_options.PriceSnapshotIndexName);

            await _indexClient.CreateOrUpdateIndexAsync(index);
        }
    }
}
