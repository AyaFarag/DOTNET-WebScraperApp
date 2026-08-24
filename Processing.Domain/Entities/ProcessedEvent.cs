using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Domain.Entities
{
    public sealed class ProcessedEvent
    {
        public Guid Id { get; private set; }

        public Guid EventId { get; private set; }

        public string EventType { get; private set; } = null!;

        public DateTime ProcessedAtUtc { get; private set; }

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
            ProcessedAtUtc = DateTime.UtcNow;
        }
    }
}
