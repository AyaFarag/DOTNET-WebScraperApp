using Index.Application.DTOs.Search;
using Index.Application.Interfaces.Search;

namespace Index.Application.Service
{
    public sealed class ProductSearchService : IProductSearchService
    {
        private readonly ISearchIndexReader _searchIndex;

        public ProductSearchService(ISearchIndexReader searchIndex)
        {
            _searchIndex = searchIndex;
        }

        public Task<SearchResultDto> SearchAsync(ProductSearchRequest request,CancellationToken cancellationToken)
        {
            return _searchIndex.SearchAsync(request, cancellationToken);
        }
    }
}
