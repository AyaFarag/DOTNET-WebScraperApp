namespace Analytics.Application.DTOs
{
    public sealed record PriceAnomalyDto
    {
        public Guid Id { get; init; }
        public Guid SnapshotId { get; init; }

        public string ProductKey { get; init; } = string.Empty;
        public string? ProductName { get; init; }
        public string? Brand { get; init; }
        public string? CategoryCode { get; init; }
        public string? Source { get; init; }


        public string Currency { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public decimal Median { get; init; }
        public decimal LowerBound { get; init; }
        public decimal UpperBound { get; init; }
        public decimal? DeviationPercentage { get; init; }



        public string Severity { get; init; } = string.Empty;
        public string Direction { get; init; } = string.Empty;
        public DateTime DetectedAt { get; init; }
    }
}
