using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.Service
{
    public interface IAnalyticsSearchService
    {
        Task<AnalyticsSearchResult> SearchAsync(
            string query,
            CancellationToken cancellationToken);
    }
}
