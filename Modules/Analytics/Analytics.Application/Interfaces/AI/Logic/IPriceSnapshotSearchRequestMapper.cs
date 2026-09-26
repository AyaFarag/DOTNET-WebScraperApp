using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.Logic
{
    public interface IPriceSnapshotSearchRequestMapper
    {
        PriceSnapshotSearchRequest Map(
            AnalyticsSearchQueryPlan plan,
            DateTime now);
    }
}
