namespace Analytics.Application.DTOs
{
    public sealed record AnalyticsExecutionDto
    {
        public Guid Id { get; init; }

        public Guid BatchId { get; init; }

        public string Status { get; init; } = string.Empty;

        public DateTime StartedAt { get; init; }

        public DateTime? CompletedAt { get; init; }

        public int ProductsProcessed { get; init; }

        public int SnapshotsCreated { get; init; }

        public int AnomaliesDetected { get; init; }

        public string? ErrorMessage { get; init; }

        public TimeSpan? Duration { get; init; }
    }
}
