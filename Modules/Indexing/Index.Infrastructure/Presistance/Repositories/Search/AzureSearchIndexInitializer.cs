using Azure;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Index.Application.Interfaces.Search;
using Index.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Index.Infrastructure.Presistance.Repositories.Search
{
    public sealed class AzureSearchIndexInitializer : IAzureSearchIndexInitializer
    {
        private readonly SearchIndexClient _indexClient;
        private readonly AzureSearchOptions _options;

        public AzureSearchIndexInitializer(SearchIndexClient indexClient,IOptions<AzureSearchOptions> options)
        {
            _indexClient = indexClient;
            _options = options.Value;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            var fields =
                new List<SearchField>
                {
                new SearchField("id", SearchFieldDataType.String)
                {
                    IsKey = true
                },

                new SearchField("productKey",SearchFieldDataType.String)
                {
                    IsSearchable = true,
                    IsFilterable = true
                },

                new SearchField("productName",SearchFieldDataType.String)
                {
                    IsSearchable = true
                },

                new SearchField("brand",SearchFieldDataType.String)
                {
                    IsSearchable = true,
                    IsFilterable = true,
                    IsFacetable = true
                },

                new SearchField("categoryCode",SearchFieldDataType.String)
                {
                    IsFilterable = true,
                    IsFacetable = true
                },

                new SearchField("subCategoryCode",SearchFieldDataType.String)
                {
                    IsFilterable = true,
                    IsFacetable = true
                },

                new SearchField("productGroupCode", SearchFieldDataType.String)
                {
                    IsFilterable = true,
                    IsFacetable = true
                },

                new SearchField("productTypeCode", SearchFieldDataType.String)
                {
                    IsFilterable = true,
                    IsFacetable = true
                },

                new SearchField("price",SearchFieldDataType.Double)
                {
                    IsFilterable = true,
                    IsSortable = true
                },

                new SearchField("currency",SearchFieldDataType.String)
                {
                    IsFilterable = true,
                    IsFacetable = true
                },

                new SearchField("unitPrice",SearchFieldDataType.Double)
                {
                    IsFilterable = true,
                    IsSortable = true
                },

                new SearchField("unit",SearchFieldDataType.String)
                {
                    IsFilterable = true
                },

                new SearchField("source",SearchFieldDataType.String)
                {
                    IsFilterable = true,
                    IsFacetable = true
                },

                new SearchField("collectedAt",SearchFieldDataType.DateTimeOffset)
                {
                    IsFilterable = true,
                    IsSortable = true
                }
                };

            var index = new SearchIndex(_options.IndexName,fields);

            await _indexClient.CreateOrUpdateIndexAsync(index);
        }
    }
}
