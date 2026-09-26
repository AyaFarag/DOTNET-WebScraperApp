namespace Classification.Application.DTOs
{
    public sealed class ProcessedProductInput
    {
        public Guid ProcessedPriceId { get; init; }

        public Guid BatchId { get; init; }

        public string ProductKey { get; init; } = null!;

        public string ProductName { get; init; } = null!;

        public string? Brand { get; init; }

        public decimal Price { get; init; }

        public string Currency { get; init; } = null!;

        public decimal? Quantity { get; init; }

        public string? Unit { get; init; }

        public decimal? UnitPrice { get; init; }

        public string? UnitPriceUnit { get; init; }
    }
}
