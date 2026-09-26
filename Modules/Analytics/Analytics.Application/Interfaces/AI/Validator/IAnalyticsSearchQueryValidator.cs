using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.Validator
{
    public interface IAnalyticsSearchQueryValidator
    {
        void Validate(AnalyticsSearchQueryPlan plan);
    }
}
