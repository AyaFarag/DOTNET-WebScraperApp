using Analytics.Application.DTOs;
using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.PriceSnapshotSearch;
using Azure.Search.Documents;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Repository
{
    public sealed class AzurePriceSnapshotSearchReader : IPriceSnapshotSearchReader
    {
        private readonly SearchClient _searchClient;

        public AzurePriceSnapshotSearchReader(SearchClient searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task<PagedResult<PriceSnapshotSearchResult>> SearchAsync(PriceSnapshotSearchRequest request, CancellationToken cancellationToken)
        {
            var options = BuildSearchOptions(request);

            var response = await _searchClient.SearchAsync<
                    PriceSnapshotSearchDocument>(
                    request.SearchText,
                    options,
                    cancellationToken);

            var results = new List<PriceSnapshotSearchResult>();

            await foreach (var result in response.Value.GetResultsAsync())
            {
                if (result.Document is null)
                    continue;

                results.Add(Map(result.Document));
            }

            var totalCount =
                response.Value.TotalCount
                ?? results.Count;

            return new PagedResult<PriceSnapshotSearchResult>(
                results,
                request.Skip / request.Take + 1,
                request.Take,
                (int)totalCount);
        }

        private static SearchOptions BuildSearchOptions(PriceSnapshotSearchRequest request)
        {
            var options = new SearchOptions
            {
                IncludeTotalCount = true,
                Skip = request.Skip,
                Size = request.Take
            };

            AddFilters(options, request);

            AddOrdering(options, request);

            return options;
        }

        private static void AddFilters(SearchOptions options,PriceSnapshotSearchRequest request)
        {
            var filters = new List<string>();

            if (!string.IsNullOrWhiteSpace(request.ProductKey))
            {
                filters.Add(
                    $"ProductKey eq '{EscapeOData(request.ProductKey)}'");
            }

            if (!string.IsNullOrWhiteSpace(request.CategoryCode))
            {
                filters.Add(
                    $"CategoryCode eq '{EscapeOData(request.CategoryCode)}'");
            }

            if (!string.IsNullOrWhiteSpace(request.Source))
            {
                filters.Add(
                    $"Source eq '{EscapeOData(request.Source)}'");
            }

            if (!string.IsNullOrWhiteSpace(request.Currency))
            {
                filters.Add(
                    $"Currency eq '{EscapeOData(request.Currency)}'");
            }

            if (!string.IsNullOrWhiteSpace(request.Brand))
            {
                filters.Add(
                    $"Brand eq '{EscapeOData(request.Brand)}'");
            }

            if (request.MinimumPrice.HasValue)
            {
                filters.Add(
                    $"Price ge {request.MinimumPrice.Value}");
            }

            if (request.MaximumPrice.HasValue)
            {
                filters.Add(
                    $"Price le {request.MaximumPrice.Value}");
            }

            if (request.From.HasValue)
            {
                filters.Add(
                    $"CollectedAt ge {request.From.Value:O}");
            }

            if (request.To.HasValue)
            {
                filters.Add(
                    $"CollectedAt le {request.To.Value:O}");
            }

            if (filters.Count > 0)
            {
                options.Filter =
                    string.Join(" and ", filters);
            }
        }

        private static void AddOrdering(SearchOptions options,  PriceSnapshotSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SortBy))
                return;

            var field = request.SortBy.ToLowerInvariant() 
                switch
            {
                "price" => "Price",
                "unitprice" => "UnitPrice",
                "collectedat" => "CollectedAt",
                null => null

            };

            if (field is null)
                return;

            options.OrderBy.Add(request.Descending
                    ? $"{field} desc"
                    : $"{field} asc");
        }

        private static PriceSnapshotSearchResult Map(PriceSnapshotSearchDocument document)
        {
            return new PriceSnapshotSearchResult
            {
                Id = document.Id,
                ProductKey = document.ProductKey,
                ProductName = document.ProductName,
                Brand = document.Brand,
                CategoryCode = document.CategoryCode,
                Source = document.Source,
                Price = (decimal)document.Price,
                UnitPrice = document.UnitPrice.HasValue
                    ? (decimal?)document.UnitPrice.Value
                    : null,
                Currency = document.Currency,
                CollectedAt = document.CollectedAt
            };
        }

        private static string EscapeOData(string value)
        {
            return value.Replace(
                "'",
                "''",
                StringComparison.Ordinal);
        }
    }
}
