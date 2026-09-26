namespace Index.Application.DTOs
{
    public sealed record IndexedProductDto
    {
        public Guid Id { get; init; }

        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public decimal Price { get; init; }

        public decimal? UnitPrice { get; init; }

        public string Currency { get; init; } = string.Empty;

        public DateTime CollectedAt { get; init; }
    }
}
