namespace Analytics.Domain.Entities
{
    public sealed class PriceSnapshot
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

        private PriceSnapshot()
        {
        }

        public PriceSnapshot(
            Guid batchId,
            string productKey,
            string? productName,
            string? brand,
            string? categoryCode,
            string? source,
            decimal price,
            decimal? unitPrice,
            string currency,
            DateTime collectedAt)
        {
            Id = Guid.NewGuid();

            BatchId = batchId;
            ProductKey = productKey;
            ProductName = productName;
            Brand = brand;
            CategoryCode = categoryCode;
            Source = source;
            Price = price;
            UnitPrice = unitPrice;
            Currency = currency;
            CollectedAt = collectedAt;
        }
    }
}
