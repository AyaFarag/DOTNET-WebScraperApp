namespace Classification.Domain.Enttities
{
    public sealed class AgentExecutionLog
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public Guid EntityId { get; private set; }

        public string AgentName { get; private set; } = null!;

        public string Model { get; private set; } = null!;

        public string Module { get; init; } = string.Empty;
        public string PromptVersion { get; private set; } = null!;

        public DateTime StartedAt { get; private set; }

        public DateTime? CompletedAt { get; private set; }

        public int LatencyMs { get; private set; }

        public int InputTokens { get; private set; }

        public int OutputTokens { get; private set; }

        public int TotalTokens { get; private set; }

        public double? Confidence { get; private set; }

        public string Status { get; private set; } = null!;

        public string? Error { get; private set; }
        public string? Input { get; private set; }

        public string? Output { get; private set; }

        private AgentExecutionLog()
        {
        }

        public AgentExecutionLog(
            Guid batchId,
            Guid entityId,
            string agentName,
            string model,
            string promptVersion,
            string input = "")
        {
            Id = Guid.NewGuid();

            BatchId = batchId;

            EntityId = entityId;

            AgentName = agentName;

            Model = model;

            PromptVersion = promptVersion;

            StartedAt = DateTime.UtcNow;

            Input = input;

            Status = "Running";

        }

        public void Complete(
            int latencyMs,
            int inputTokens,
            int outputTokens,
            double confidence,
            string output = "")
        {
            CompletedAt = DateTime.UtcNow;

            LatencyMs = latencyMs;

            InputTokens = inputTokens;

            OutputTokens = outputTokens;

            TotalTokens =
            InputTokens + OutputTokens;

            Confidence = confidence;

            Output = output;

            Status = "Completed";
        }

        public void Fail(
            int latencyMs,
            string error)
        {
            CompletedAt = DateTime.UtcNow;

            LatencyMs = latencyMs;

            Status = "Failed";

            Error = error;
        }
    }
}
