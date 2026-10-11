namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record PriceSnapshotSearchRequest
    {
        public string? SearchText { get; init; }

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

        public string? SortBy { get; init; }

        public bool Descending { get; init; }

        public int Skip { get; init; }

        public int Take { get; init; }
    }
}
