namespace Analytics.Application.Interfaces.PriceAnalytics.Service
{
    public interface IAnalyticsService
    {
        Task CreatePriceSnapshotsAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
