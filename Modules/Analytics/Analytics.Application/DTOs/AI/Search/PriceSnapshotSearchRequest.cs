namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record PriceSnapshotSearchRequest
    {
        public string? ProductKey { get; init; }

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public string? Currency { get; init; }

        public decimal? MinimumPrice { get; init; }

        public decimal? MaximumPrice { get; init; }

        public DateTime? From { get; init; }

        public DateTime? To { get; init; }

        public int Page { get; init; } = 1;

        public int PageSize { get; init; } = 20;
    }
}
