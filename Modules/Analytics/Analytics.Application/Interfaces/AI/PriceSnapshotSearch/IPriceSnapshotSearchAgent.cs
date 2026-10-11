using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.PriceSnapshotSearch
{
    public interface IPriceSnapshotSearchAgent
    {
        Task<PriceSnapshotSearchRequest> CreateRequestAsync(
            AnalyticsSearchQueryPlan plan,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
