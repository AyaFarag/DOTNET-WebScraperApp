using MediatR;

namespace Shared.Contracts.Events.Processing
{
    public sealed record PriceDataProcessedEvent(
    Guid EventId,
    Guid BatchId,
    int TotalRecords,
    int ProcessedRecords,
    int FailedRecords
) : INotification;
}
