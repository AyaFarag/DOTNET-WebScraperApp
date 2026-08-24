namespace Processing.Domain.Entities
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; private set; }

        public Guid EventId { get; private set; }

        public string EventType { get; private set; } = null!;

        public string Payload { get; private set; } = null!;

        public DateTime OccurredOnUtc { get; private set; }

        public DateTime? ProcessedOnUtc { get; private set; }

        public int RetryCount { get; private set; }

        public string? Error { get; private set; }

        private OutboxMessage()
        {
        }

        public OutboxMessage(
            Guid eventId,
            string eventType,
            string payload)
        {
            Id = Guid.NewGuid();
            EventId = eventId;
            EventType = eventType;
            Payload = payload;
            OccurredOnUtc = DateTime.UtcNow;
        }

        public void MarkAsProcessed()
        {
            ProcessedOnUtc = DateTime.UtcNow;
            Error = null;
        }

        public void MarkAsFailed(string error)
        {
            RetryCount++;
            Error = error;
        }
    }
}
