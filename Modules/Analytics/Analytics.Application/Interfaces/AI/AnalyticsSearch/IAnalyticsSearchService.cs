using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.AnalyticsSearch
{
    public interface IAnalyticsSearchService
    {
        Task<AnalyticsSearchResult> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
