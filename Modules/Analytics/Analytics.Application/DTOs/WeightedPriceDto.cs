namespace Analytics.Application.DTOs
{
    public sealed record WeightedPriceDto
    {
        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public string Currency { get; init; } = string.Empty;

        public decimal Price { get; init; }

        public decimal Weight { get; init; }

        public decimal WeightPercentage { get; init; }

        public decimal WeightedValue { get; init; }

        public DateTime PriceDate { get; init; }
    }
}
