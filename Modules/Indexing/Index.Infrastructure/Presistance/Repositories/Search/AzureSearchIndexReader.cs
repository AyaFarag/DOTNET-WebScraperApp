using Azure.Search.Documents;
using Index.Application.DTOs;
using Index.Application.DTOs.Search;
using Index.Application.Interfaces.Search;


namespace Index.Infrastructure.Presistance.Repositories.Search
{

    public sealed class AzureSearchIndexReader : ISearchIndexReader
    {
        private readonly SearchClient _client;

        public AzureSearchIndexReader(SearchClient client)
        {
            _client = client;
        }

        // search here -----------------
        public async Task<SearchResultDto> SearchAsync(ProductSearchRequest request,CancellationToken cancellationToken)
        {
            var options = new SearchOptions
            {
                IncludeTotalCount = true,

                Skip =
                    (request.Page - 1) *
                    request.PageSize,

                Size = request.PageSize
            };

            // Filters
            var filters = new List<string>();

            if (!string.IsNullOrWhiteSpace(request.CategoryCode))
            {
                filters.Add($"categoryCode eq '{Escape(request.CategoryCode)}'");
            }

            if (!string.IsNullOrWhiteSpace(request.SubCategoryCode))
            {
                filters.Add($"subCategoryCode eq '{Escape(request.SubCategoryCode)}'");
            }

            if (!string.IsNullOrWhiteSpace(request.Brand))
            {
                filters.Add($"brand eq '{Escape(request.Brand)}'");
            }

            if (!string.IsNullOrWhiteSpace(request.Source))
            {
                filters.Add($"source eq '{Escape(request.Source)}'");
            }

            if (!string.IsNullOrWhiteSpace(request.Currency))
            {
                filters.Add($"currency eq '{Escape(request.Currency)}'");
            }

            if (request.MinPrice.HasValue)
            {
                filters.Add($"price ge {request.MinPrice.Value}");
            }

            if (request.MaxPrice.HasValue)
            {
                filters.Add($"price le {request.MaxPrice.Value}");
            }

            if (filters.Count > 0)
            {
                options.Filter = string.Join(" and ", filters);
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(
                request.SortBy))
            {
                var direction =request.Descending
                        ? "desc"
                        : "asc";

                options.OrderBy.Add($"{request.SortBy} {direction}");
            }

            // Search
            var searchText = string.IsNullOrWhiteSpace(request.Query)
                    ? "*"
                    : request.Query;

            var response = await _client.SearchAsync<ProductSearchDocument>(searchText, options, cancellationToken);

            var results = new List<ProductSearchResultDto>();

            await foreach (var result in response.Value.GetResultsAsync())
            {
                var document = result.Document;

                results.Add( new ProductSearchResultDto
                    {
                        Id = document.Id,

                        ProductKey = document.ProductKey,

                        ProductName = document.ProductName,

                        Brand = document.Brand,

                        CategoryCode = document.CategoryCode,

                        SubCategoryCode = document.SubCategoryCode,

                        ProductTypeCode = document.ProductTypeCode,

                        Price = document.Price,

                        Currency = document.Currency,

                        UnitPrice = document.UnitPrice,

                        Unit = document.Unit,

                        Source = document.Source,

                        CollectedAt = document.CollectedAt
                    });
            }

            return new SearchResultDto
            {
                Items = results,

                TotalCount = (int)(response.Value.TotalCount ?? 0),

                Page = request.Page,

                PageSize = request.PageSize
            };
        }

        private static string Escape(string value)
        {
            return value.Replace(
                "'",
                "''");
        }
    }
}
