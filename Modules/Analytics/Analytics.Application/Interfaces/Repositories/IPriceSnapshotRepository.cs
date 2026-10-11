using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.Repositories
{
    public interface IPriceSnapshotRepository
    {
        Task AddRangeAsync(IReadOnlyCollection<PriceSnapshot> snapshots,CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceSnapshot>> GetByProductKeyAsync(string productKey, string? source, string? currency, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceSnapshot>> GetForAnalyticsAsync(string? categoryCode, string? source, string? Currency, CancellationToken cancellationToken);

        Task<PriceSnapshot?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceSnapshot>> GetHistoricalAsync(string productKey,string? source,string currency,DateTime before,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceSnapshot>> GetByBatchIdAsync(Guid batchId,CancellationToken cancellationToken);

        Task AddAsync(PriceSnapshot snapshot,CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceSnapshot>> GetProductHistoryAsync(string productKey,DateTime from,DateTime to,string? source,string? currency, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceSnapshot>> GetLatestPricesByProductAsync(string productKey,string? currency,CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceSnapshot>>
            GetAsync(
                string? categoryCode,
                string? source,
                string currency,
                DateTime from,
                DateTime to,
                CancellationToken cancellationToken);
        Task<PriceSnapshot?> GetLatestAtOrBeforeAsync(
            string productKey,
            DateTime date,
            string? source,
            string currency,
            CancellationToken cancellationToken);
    }
}
