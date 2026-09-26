using MediatR;

namespace Shared.Contracts.Events.Index
{
    public sealed record PriceDataIndexedEvent(
    Guid EventId,
    Guid BatchId) : INotification;
}
