using MediatR;

namespace Shared.Contracts.Events.Analytics
{
    public sealed record AnalyticsCompletedEvent(
    Guid EventId,
    Guid BatchId) : INotification;
}
