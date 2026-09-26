using MediatR;

namespace Shared.Contracts.Events.Analytics
{
    public sealed record AnomalyDetectedEvent(
    Guid EventId,
    Guid AnomalyId) : INotification;
}
