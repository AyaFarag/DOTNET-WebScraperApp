using Index.Application.DTOs.Search;

namespace Index.Application.Interfaces.Search
{
    public interface ISearchQueryPlanValidator
    {
        Task<SearchQueryPlan> ValidateAsync(SearchQueryPlan plan, CancellationToken cancellationToken);
    }
}
