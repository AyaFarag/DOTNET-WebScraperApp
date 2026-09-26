using Index.Application.DTOs.Search;

namespace Index.Application.Interfaces.Search
{
    public interface ISearchQueryAgent
    {
        Task<SearchQueryPlan> CreateQueryPlanAsync(string userQuery,CancellationToken cancellationToken);
    }
}
