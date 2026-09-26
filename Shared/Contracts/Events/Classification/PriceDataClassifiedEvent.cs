using MediatR;

namespace Shared.Contracts.Events.Classification
{
    public sealed record PriceDataClassifiedEvent(
    Guid EventId,
    Guid BatchId) : INotification;
}
