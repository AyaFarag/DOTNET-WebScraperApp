namespace Shared.Domain
{
    public class RawPrice
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public string Source { get; private set; } = null!;

        public string SourceUrl { get; private set; } = null!;

        public string ProductName { get; private set; } = null!;

        public string RawPriceValue { get; private set; } = null!;

        public string? Currency { get; private set; }

        public DateTime CollectedAt { get; private set; }

        public string? RawData { get; private set; }

        private RawPrice()
        {
        }

        public RawPrice(
            Guid batchId,
            string source,
            string sourceUrl,
            string productName,
            string rawPriceValue,
            string? currency,
            DateTime collectedAt,
            string? rawData)
        {
            Id = Guid.NewGuid();

            BatchId = batchId;
            Source = source;
            SourceUrl = sourceUrl;
            ProductName = productName;
            RawPriceValue = rawPriceValue;
            Currency = currency;
            CollectedAt = collectedAt;
            RawData = rawData;
        }

    }
}
