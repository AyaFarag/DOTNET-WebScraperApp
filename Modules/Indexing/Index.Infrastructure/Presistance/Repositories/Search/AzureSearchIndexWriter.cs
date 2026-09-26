using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Index.Application.DTOs;
using Index.Application.Interfaces.Search;

namespace Index.Infrastructure.Presistance.Repositories.Search
{


    public sealed class AzureSearchIndexWriter : ISearchIndexWriter
    {
        private readonly SearchClient _client;

        public AzureSearchIndexWriter(SearchClient client)
        {
            _client = client;
        }

        public async Task IndexAsync(ProductSearchDocument document,CancellationToken cancellationToken)
        {
            var batch =IndexDocumentsBatch.Create(IndexDocumentsAction.Upload(document));

            await _client.IndexDocumentsAsync(batch, cancellationToken: cancellationToken);
        }

        public async Task IndexBatchAsync(IReadOnlyCollection<ProductSearchDocument> documents,
            CancellationToken cancellationToken)
        {
            if (documents.Count == 0)
                return;

            var batch = IndexDocumentsBatch.Create(IndexDocumentsAction.Upload(documents));

            await _client.IndexDocumentsAsync(batch, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync( string documentId, CancellationToken cancellationToken)
        {
            var batch = IndexDocumentsBatch.Create(IndexDocumentsAction.Delete(
                        new ProductSearchDocument
                        {
                            Id = documentId
                        }));

            await _client.IndexDocumentsAsync(batch,cancellationToken: cancellationToken);
        }
    }
}
