namespace Classification.Domain.Enttities
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; private set; }

        public string Type { get; private set; } = null!;

        public string Payload { get; private set; } = null!;

        public DateTime OccurredOnUtc { get; private set; }

        public DateTime? ProcessedOnUtc { get; private set; }

        public int RetryCount { get; private set; }

        public string? Error { get; private set; }

        public DateTime? NextAttemptOnUtc { get; private set; }

        public bool IsProcessed =>
            ProcessedOnUtc.HasValue;

        private OutboxMessage()
        {
        }

        public OutboxMessage(string type,string payload)
        {
            Id = Guid.NewGuid();

            Type = type;
            Payload = payload;

            OccurredOnUtc = DateTime.UtcNow;

            RetryCount = 0;
        }

        public void MarkAsProcessed()
        {
            ProcessedOnUtc = DateTime.UtcNow;
            Error = null;
        }

        public void MarkAsFailed(string error, DateTime nextAttemptOnUtc)
        {
            RetryCount++;

            Error = error;

            NextAttemptOnUtc = nextAttemptOnUtc;
        }
    }
}
