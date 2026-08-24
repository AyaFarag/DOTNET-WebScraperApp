using MediatR;

namespace Shared.Contracts.Events.Ingestion
{
    public class PriceScrapedEvent : BaseEvent
    {
        public Guid BatchId { get; init; }
    }
    public sealed record PriceDataCollectedEvent(
    Guid EventId,
    Guid BatchId) : INotification;
}
