using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.Repository
{
    public interface IPriceSnapshotSearchReader
    {
        Task<AnalyticsSearchResult> SearchAsync(
            PriceSnapshotSearchRequest request,
            CancellationToken cancellationToken);
    }
}
