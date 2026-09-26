using Analytics.Application.Enums;

namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record ProductSearchQueryPlan
    {
        public string? SearchText { get; init; }

        public string? ProductKey { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public string? Currency { get; init; }

        public decimal? MinimumPrice { get; init; }

        public decimal? MaximumPrice { get; init; }

        public ProductSearchSortBy SortBy { get; init; }
            = ProductSearchSortBy.Relevance;

        public SearchSortDirection SortDirection { get; init; }
            = SearchSortDirection.Descending;

        public int Page { get; init; } = 1;

        public int PageSize { get; init; } = 20;
    }
}
