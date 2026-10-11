using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.PriceSnapshotSearch
{
    public interface IPriceSnapshotSearchRequestMapper
    {
        PriceSnapshotSearchRequest Map(AnalyticsSearchQueryPlan plan,DateTime now);
    }
}
