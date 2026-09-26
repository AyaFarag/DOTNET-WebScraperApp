using MediatR;

namespace Shared.Contracts.Events.Validation
{
    public sealed record PriceDataValidatedEvent(
        Guid EventId,
        Guid BatchId,
        int TotalRecords,
        int ValidRecords,
        int InvalidRecords
    ) : INotification;





}
