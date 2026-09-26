using MediatR;

namespace Analytics.Application.Events
{
    public sealed record AnomalyDetectedEvent(
    Guid EventId,
    Guid AnomalyId) : INotification;
}
