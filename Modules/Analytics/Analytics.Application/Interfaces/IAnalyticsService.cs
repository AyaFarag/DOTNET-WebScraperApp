namespace Analytics.Application.Interfaces
{
    public interface IAnalyticsService
    {
        Task CreatePriceSnapshotsAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
