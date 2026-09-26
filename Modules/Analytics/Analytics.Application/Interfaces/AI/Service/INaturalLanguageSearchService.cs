using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.Service
{
    public interface INaturalLanguageSearchService
    {
        Task<ProductSearchResult> SearchAsync(string userQuery,CancellationToken cancellationToken);
    }
}
