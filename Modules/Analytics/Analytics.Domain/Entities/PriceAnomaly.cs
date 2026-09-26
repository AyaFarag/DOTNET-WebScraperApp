namespace Analytics.Domain.Entities
{
    public sealed class PriceAnomaly
    {
        public Guid Id { get; private set; }

        public Guid SnapshotId { get; private set; }

        public string ProductKey { get; private set; } = string.Empty;

        public string? ProductName { get; private set; }

        public string? Brand { get; private set; }

        public string? CategoryCode { get; private set; }

        public string? Source { get; private set; }

        public string Currency { get; private set; } = string.Empty;

        public decimal Price { get; private set; }

        public decimal Q1 { get; private set; }

        public decimal Median { get; private set; }

        public decimal Q3 { get; private set; }

        public decimal Iqr { get; private set; }

        public decimal LowerBound { get; private set; }

        public decimal UpperBound { get; private set; }

        public decimal? DeviationPercentage { get; private set; }

        public string Severity { get; private set; } = string.Empty;
        public string Direction { get; private set; } = string.Empty;

        public DateTime DetectedAt { get; private set; }

        private PriceAnomaly()
        {
        }

        public PriceAnomaly(
            Guid snapshotId,
            string productKey,
            string? productName,
            string? brand,
            string? categoryCode,
            string? source,
            string currency,
            decimal price,
            AnomalyDetectionResult result)
        {
            Id = Guid.NewGuid();

            SnapshotId = snapshotId;
            ProductKey = productKey;
            ProductName = productName;
            Brand = brand;
            CategoryCode = categoryCode;
            Source = source;
            Currency = currency;

            Price = price;

            Q1 = result.Q1;
            Median = result.Median;
            Q3 = result.Q3;
            Iqr = result.Iqr;
            LowerBound = result.LowerBound;
            UpperBound = result.UpperBound;
            DeviationPercentage = result.DeviationPercentage;
            Severity = result.Severity;
            Direction = result.Direction;
            DetectedAt = DateTime.UtcNow;
        }
    }
}
