namespace Analytics.Application.DTOs.AI.Search
{
    public sealed class PriceSnapshotSearchDocument
    {
        public string Id { get; set; } = string.Empty;

        public string ProductKey { get; set; } = string.Empty;

        public string? ProductName { get; set; }

        public string? Brand { get; set; }

        public string? CategoryCode { get; set; }

        public string? Source { get; set; }

        public decimal Price { get; set; }

        public decimal? UnitPrice { get; set; }

        public string Currency { get; set; } = string.Empty;

        public DateTime CollectedAt { get; set; }
    }
}
