using Index.Application.DTOs;

namespace Index.Application.Interfaces.Search
{
    public interface ISearchIndexWriter
    {
        Task IndexAsync(ProductSearchDocument document, CancellationToken cancellationToken);

        Task IndexBatchAsync(IReadOnlyCollection<ProductSearchDocument> documents,CancellationToken cancellationToken);

        Task DeleteAsync(string documentId, CancellationToken cancellationToken);


    }
}
