namespace Analytics.Domain.Entities
{
    public sealed record AnomalyDetectionResult
    {
        public bool IsAnomaly { get; init; }

        public string Severity { get; init; } = string.Empty;

        public decimal Price { get; init; }

        public decimal Q1 { get; init; }

        public decimal Median { get; init; }

        public decimal Q3 { get; init; }

        public decimal Iqr { get; init; }

        public decimal LowerBound { get; init; }

        public decimal UpperBound { get; init; }

        public decimal? DeviationPercentage { get; init; }
        public string Direction { get; set; } = string.Empty;
    }
}
