namespace Processing.Application.DTOs
{
    public sealed class RawPriceData

    {
        public Guid RawPriceId { get; set; }
        public Guid BatchId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Brand { get; set; }

        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;

        public decimal? Quantity { get; set; }
        public string? Unit { get; set; }

        public string Source { get; set; } = string.Empty;
        public string? ProductUrl { get; set; }

        public DateTime CollectedAtUtc { get; set; }
    }
}
