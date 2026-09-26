namespace Classification.Application.DTOs
{
    public sealed class AgentExecutionResult<T>
    {
        public T Result { get; init; } = default!;

        public int InputTokens { get; init; }

        public int OutputTokens { get; init; }

        public int TotalTokens { get; init; }

        public int LatencyMs { get; init; }

        public string Model { get; init; } = null!;
    }
}
