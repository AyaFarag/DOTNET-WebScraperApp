using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.SearchQuery
{
    public interface ISearchQueryValidator
    {
        ProductSearchRequest Validate(SearchQueryPlan plan);
    }
}
