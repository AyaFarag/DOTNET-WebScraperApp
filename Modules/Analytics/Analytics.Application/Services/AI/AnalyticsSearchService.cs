using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.AnalyticsSearch;
using Analytics.Application.Interfaces.AI.PriceSnapshotSearch;
using Analytics.Domain.Enums;

namespace Analytics.Application.Services.AI
{
    public sealed class AnalyticsSearchService
        : IAnalyticsSearchService
    {
        private readonly IAnalyticsSearchQueryAgent _analyticsSearchAgent;

        private readonly IPriceSnapshotSearchAgent  _priceSnapshotSearchAgent;

        private readonly IPriceSnapshotSearchReader
            _priceSnapshotSearchReader;

        public AnalyticsSearchService(
            IAnalyticsSearchQueryAgent analyticsSearchAgent,
            IPriceSnapshotSearchAgent priceSnapshotSearchAgent,
            IPriceSnapshotSearchReader priceSnapshotSearchReader)
        {
            _analyticsSearchAgent = analyticsSearchAgent;
            _priceSnapshotSearchAgent = priceSnapshotSearchAgent;
            _priceSnapshotSearchReader = priceSnapshotSearchReader;
        }

        public async Task<AnalyticsSearchResult> SearchAsync(
            string query,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            ValidateRequest(query, page, pageSize);

            // -----------------------------------------
            // STEP 1
            // Understand the user's question
            // -----------------------------------------

            var queryPlan =
                await _analyticsSearchAgent.CreatePlanAsync(
                    query,
                    cancellationToken);

            // -----------------------------------------
            // STEP 2
            // We currently execute Price Snapshot Search
            // -----------------------------------------

            if (queryPlan.Intent !=
                AnalyticsSearchIntent.PriceSnapshotSearch)
            {
                throw new InvalidOperationException(
                    $"Search intent '{queryPlan.Intent}' " +
                    "is not supported by the Price Snapshot " +
                    "search pipeline yet.");
            }

            // -----------------------------------------
            // STEP 3
            // Create Azure Search strategy
            // -----------------------------------------

            var searchRequest =
                await _priceSnapshotSearchAgent.CreateRequestAsync(
                    queryPlan,
                    page,
                    pageSize,
                    cancellationToken);

            // -----------------------------------------
            // STEP 4
            // Execute deterministic search
            // -----------------------------------------

            var searchResult =
                await _priceSnapshotSearchReader.SearchAsync(
                    searchRequest,
                    cancellationToken);

            // -----------------------------------------
            // STEP 5
            // Return final result
            // -----------------------------------------

            return new AnalyticsSearchResult
            {
                Query = query,
                QueryPlan = queryPlan,
                Page = searchResult.Page,
                PageSize = searchResult.PageSize,
                TotalCount = searchResult.TotalCount,
                Items = searchResult.Items
            };
        }

        private static void ValidateRequest(
            string query,
            int page,
            int pageSize)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException(
                    "Search query is required.",
                    nameof(query));
            }

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(page),
                    "Page must be greater than zero.");
            }

            if (pageSize < 1 || pageSize > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    "Page size must be between 1 and 100.");
            }
        }
    }
}
