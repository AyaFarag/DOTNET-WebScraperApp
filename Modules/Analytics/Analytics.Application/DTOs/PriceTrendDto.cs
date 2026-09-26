namespace Analytics.Application.DTOs
{
    public sealed record PriceTrendDto
    {
        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? Source { get; init; }

        public string Currency { get; init; } = string.Empty;

        public DateTime PeriodStart { get; init; }

        public DateTime PeriodEnd { get; init; }

        public decimal OpeningPrice { get; init; }

        public decimal ClosingPrice { get; init; }

        public decimal MinimumPrice { get; init; }

        public decimal MaximumPrice { get; init; }

        public decimal AveragePrice { get; init; }

        public decimal PriceChange { get; init; }

        public decimal? PriceChangePercentage { get; init; }
    }
}
