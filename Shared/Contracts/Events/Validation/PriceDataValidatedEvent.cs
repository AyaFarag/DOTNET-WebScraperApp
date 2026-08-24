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


    public sealed record ValidatedPriceData
    {
        public Guid RawPriceId { get; set; }
        public Guid BatchId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }


}
