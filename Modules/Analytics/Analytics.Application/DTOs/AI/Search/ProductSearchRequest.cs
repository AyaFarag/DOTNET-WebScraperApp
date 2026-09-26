using Analytics.Application.Enums;

namespace Analytics.Application.DTOs.AI.Search
{
       public sealed record ProductSearchRequest
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

    //public sealed record ProductSearchResult
    //{
    //    public string? SearchText { get; init; }

    //    public string? ProductKey { get; init; }

    //    public string? ProductName { get; init; }

    //    public string? Brand { get; init; }

    //    public string? CategoryCode { get; init; }

    //    public string? Source { get; init; }

    //    public string? Currency { get; init; }

    //    public decimal? MinimumPrice { get; init; }

    //    public decimal? MaximumPrice { get; init; }

    //    public string? SortBy { get; init; }

    //    public string? SortDirection { get; init; }

    //    public int Page { get; init; }

    //    public int PageSize { get; init; }
    //}
}
