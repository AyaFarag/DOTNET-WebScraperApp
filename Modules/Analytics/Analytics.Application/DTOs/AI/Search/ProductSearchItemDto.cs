namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record ProductSearchItemDto
    {
        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public decimal Price { get; init; }

        public string Currency { get; init; } = string.Empty;

        public DateTime? LastCollectedAt { get; init; }

        public double? SearchScore { get; init; }
    }
}
