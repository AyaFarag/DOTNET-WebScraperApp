

namespace Shared.Contracts.Events.Ingestion
{
    public class EmailScrapedEvent : BaseEvent
    {
        public string Email { get; set; } = "";
    }
}
