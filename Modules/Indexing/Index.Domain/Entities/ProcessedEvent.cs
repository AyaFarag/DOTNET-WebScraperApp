namespace Index.Domain.Entities
{
    public sealed class ProcessedEvent
    {
        public Guid Id { get; private set; }

        public Guid EventId { get; private set; }

        public string EventType { get; private set; } = null!;

        public DateTime ProcessedAt { get; private set; }

        private ProcessedEvent()
        {
        }

        public ProcessedEvent(
            Guid eventId,
            string eventType)
        {
            Id = Guid.NewGuid();

            EventId = eventId;

            EventType = eventType;

            ProcessedAt = DateTime.UtcNow;
        }
    }
}
