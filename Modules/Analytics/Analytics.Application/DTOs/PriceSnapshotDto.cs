namespace Analytics.Application.DTOs
{
    public class PriceSnapshotDto
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public string ProductKey { get; private set; } = string.Empty;

        public string? ProductName { get; private set; }

        public string? Brand { get; private set; }

        public string? CategoryCode { get; private set; }

        public string? Source { get; private set; }

        public decimal Price { get; private set; }

        public decimal? UnitPrice { get; private set; }

        public string Currency { get; private set; } = string.Empty;

        public DateTime CollectedAt { get; private set; }
    }
}
