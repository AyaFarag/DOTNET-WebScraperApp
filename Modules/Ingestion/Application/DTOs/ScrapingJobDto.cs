namespace Application.DTOs
{
    public sealed class ScrapingJobDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public Guid SourceId { get; init; }

        public string SourceName { get; init; } = string.Empty;

        public string Status { get; init; } = string.Empty;

        public bool IsEnabled { get; init; }

        public string? Schedule { get; init; }

        public DateTime? LastRunAt { get; init; }

        public DateTime? NextRunAt { get; init; }

        public DateTime? LastSucceededAt { get; init; }

        public DateTime? LastFailedAt { get; init; }

        public int TotalExecutions { get; init; }

        public int SuccessfulExecutions { get; init; }

        public int FailedExecutions { get; init; }
    }
}
