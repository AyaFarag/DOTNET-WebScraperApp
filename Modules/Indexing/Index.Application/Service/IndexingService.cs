using Index.Application.CQRS.Command;
using Index.Application.DTOs;
using Index.Application.Interfaces;
using Index.Application.Interfaces.Repository;
using Index.Application.Interfaces.Search;
using Index.Application.Interfaces.Service;
using Index.Application.Mapper;
using Index.Domain.Entities;
using Shared.Contracts.Events.Index;
using Shared.Contracts.Queries.Classification;
using System.Text.Json;

namespace Index.Application.Service
{
    public sealed class IndexingService : IIndexingService
    {
        private readonly IClassifiedProductReader<ClassifiedProductInput> _productReader;
        private readonly ISearchIndexWriter _searchIndex;
        private readonly IIndexingRepository _indexingRepository;
        private readonly IIndexingExecutionRepository _executionRepository;
        private readonly IIndexingOutboxMessage _outboxRepository;
        private readonly IIndexingUnitOfWork _unitOfWork;
        public IndexingService(
            IClassifiedProductReader<ClassifiedProductInput> productReader,
            IIndexingRepository indexingRepository,
            IIndexingExecutionRepository executionRepository,
            IIndexingOutboxMessage outboxRepository,
            IIndexingUnitOfWork unitOfWork)
        {
            _productReader = productReader;
            _indexingRepository = indexingRepository;
            _executionRepository = executionRepository;
            _outboxRepository = outboxRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IndexingResultDto> IndexBatchAsync(Guid batchId,CancellationToken cancellationToken)
        {
            // 1. Read classified products
            var products = await _productReader.GetByBatchIdAsync(batchId,cancellationToken);

            if (products.Count == 0)
            {
                throw new InvalidOperationException(
                    $"No classified products found for batch {batchId}.");
            }

            // 2. Create execution
            var execution =new IndexingExecution(batchId, products.Count);

            await _executionRepository.AddAsync(execution,cancellationToken);

            try
            {
                // 3. Process products
                foreach (var product in products)
                {
                    try
                    {
                        // Additional protection against duplicates
                        var exists = await _indexingRepository.ExistsAsync(product.ClassificationResultId,cancellationToken);

                        if (exists)
                        {
                            continue;
                        }

                        // 4. Create IndexedProduct
                        var indexedProduct =
                            new IndexedProduct(
                                batchId,
                                product.ClassificationResultId,
                                product.ProcessedPriceId,
                                product.ProductKey,
                                product.ProductName,
                                product.Brand,
                                product.CategoryCode,
                                product.SubCategoryCode,
                                product.ProductGroupCode,
                                product.ProductTypeCode,
                                product.Price,
                                product.Currency,
                                product.UnitPrice,
                                product.Unit,
                                product.Source,
                                product.CollectedAt);

                        // 5. Add to repository
                        await _indexingRepository.AddAsync(indexedProduct,cancellationToken);
                        
                        var indexCommand = new IndexProductCommand(Guid.NewGuid(),indexedProduct.Id);
                       
                        // 8. Publish Indexed event through Outbox
                        var indexedEvent = new PriceDataIndexedEvent(Guid.NewGuid(), batchId);

                        var outboxMessage = new OutboxMessage(indexCommand.EventId,
                            nameof(PriceDataIndexedEvent), JsonSerializer.Serialize(indexedEvent));
                        await _outboxRepository.AddAsync(outboxMessage, cancellationToken);

                        // 6. Update execution
                        execution.IncrementIndexed();
                    }
                    catch
                    {
                        execution.IncrementFailed();
                    }
                }

                // 7. Complete execution
                if (execution.FailedProducts > 0)
                {
                    execution.Fail("Completed With Errors");
                }
                else
                {
                    execution.Complete();
                }
                // 9. One transaction
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new IndexingResultDto
                {
                    BatchId = batchId,

                    ExecutionId = execution.Id,

                    TotalProducts = execution.TotalProducts,

                    IndexedProducts = execution.IndexedProducts,

                    FailedProducts = execution.FailedProducts,

                    Status = execution.Status.ToString()
                };
            }
            catch (Exception ex)
            {
                execution.Fail(ex.Message);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                throw;
            }
        }
    }
}
