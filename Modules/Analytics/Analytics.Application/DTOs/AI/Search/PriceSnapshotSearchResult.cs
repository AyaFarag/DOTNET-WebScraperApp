namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record PriceSnapshotSearchResult
    {
        public string Id { get; init; } = string.Empty;

        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public decimal Price { get; init; }

        public decimal? UnitPrice { get; init; }

        public string Currency { get; init; } = string.Empty;

        public DateTimeOffset CollectedAt { get; init; }
    }
}
