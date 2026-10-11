using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.SearchQuery
{
    public interface INaturalLanguageSearchService
    {
        Task<ProductSearchResult> SearchAsync(string userQuery,CancellationToken cancellationToken);
    }
}
