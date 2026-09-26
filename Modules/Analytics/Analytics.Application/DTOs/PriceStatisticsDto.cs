namespace Analytics.Application.DTOs
{
    public sealed record PriceStatisticsDto
    {
        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public string Currency { get; init; } = string.Empty;

        public decimal CurrentPrice { get; init; }

        public decimal? PreviousPrice { get; init; }

        public decimal MinimumPrice { get; init; }

        public decimal MaximumPrice { get; init; }

        public decimal AveragePrice { get; init; }

        public decimal MedianPrice { get; init; }

        public decimal? PriceChange { get; init; }

        public decimal? PriceChangePercentage { get; init; }

        public int ObservationCount { get; init; }

        public DateTime LastCollectedAt { get; init; }
    }
}
