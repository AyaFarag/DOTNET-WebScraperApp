namespace Analytics.Application.DTOs
{
    public sealed record PriceRelativeDto
    {
        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public string Currency { get; init; } = string.Empty;

        public decimal BasePrice { get; init; }

        public decimal CurrentPrice { get; init; }

        public decimal RelativeValue { get; init; }

        public decimal PriceChangePercentage { get; init; }

        public DateTime BaseDate { get; init; }

        public DateTime CurrentDate { get; init; }
    }
}
