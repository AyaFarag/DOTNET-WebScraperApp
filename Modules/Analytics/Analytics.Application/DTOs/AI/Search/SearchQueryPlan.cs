namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record SearchQueryPlan
    {
        public string SearchText { get; init; } = string.Empty;

        public string? ProductKey { get; init; }

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public string? Currency { get; init; }

        public decimal? MinimumPrice { get; init; }

        public decimal? MaximumPrice { get; init; }

        public string? SortBy { get; init; }

        public string? SortDirection { get; init; }

        public int Page { get; init; } = 1;

        public int PageSize { get; init; } = 20;
    }
}
