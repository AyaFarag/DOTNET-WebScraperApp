namespace Validation.Domain.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; private set; }

        public string Type { get; private set; } = null!;

        public string Payload { get; private set; } = null!;

        public DateTime OccurredOnUtc { get; private set; }

        public DateTime? ProcessedOnUtc { get; private set; }

        public int RetryCount { get; private set; }

        public string? Error { get; private set; }

        private OutboxMessage()
        {
        }

        public OutboxMessage(
            Guid id,
            string type,
            string payload)
        {
            Id = id;
            Type = type;
            Payload = payload;
            OccurredOnUtc = DateTime.UtcNow;
        }

        public void MarkProcessed()
        {
            ProcessedOnUtc = DateTime.UtcNow;
            Error = null;
        }

        public void MarkFailed(string error)
        {
            RetryCount++;
            Error = error;
        }
    }
}
