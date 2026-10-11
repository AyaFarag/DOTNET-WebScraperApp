using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.AnalyticsSearch
{
    public interface IAnalyticsSearchQueryValidator
    {
        void Validate(AnalyticsSearchQueryPlan plan);
    }
}
