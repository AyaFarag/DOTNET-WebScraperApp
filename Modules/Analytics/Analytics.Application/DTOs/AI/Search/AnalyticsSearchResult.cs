namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record AnalyticsSearchResult
    {
        public string Query { get; init; } = string.Empty;

        public AnalyticsSearchQueryPlan QueryPlan { get; init; } = default!;

        public int Page { get; init; }

        public int PageSize { get; init; }

        public int TotalCount { get; init; }

        public int TotalPages =>
            (int)Math.Ceiling(
                TotalCount / (double)PageSize);

        public IReadOnlyCollection<PriceSnapshotSearchResult> Items { get; init; }
            = [];
    }
}
