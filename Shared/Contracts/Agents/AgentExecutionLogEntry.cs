namespace Shared.Contracts.Agents
{
    public sealed record AgentExecutionLogEntry
    {
        public Guid Id { get; init; }

        public Guid? BatchId { get; init; }

        public Guid? EntityId { get; init; }
        
        public string AgentName { get; init; } = string.Empty;

        public string Module { get; init; } = string.Empty;

        public string? Model { get; init; }

        public string? PromptVersion { get; init; }

        public DateTime StartedAt { get; init; }

        public DateTime? CompletedAt { get; init; }

        public long? LatencyMs { get; init; }

        public int? InputTokens { get; init; }

        public int? OutputTokens { get; init; }

        public int? TotalTokens { get; init; }

        public double? Confidence { get; init; }

        public string Status { get; init; } = string.Empty;

        public string? Error { get; init; }

        public string? Input { get; init; }

        public string? Output { get; init; }
    }
}
