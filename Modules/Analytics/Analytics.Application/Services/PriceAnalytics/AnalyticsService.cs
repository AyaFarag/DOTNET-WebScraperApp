using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using Shared.Contracts.Queries.Indexing;

namespace Analytics.Application.Services.PriceAnalytics
{
    public sealed class AnalyticsService : IAnalyticsService
    {
        private readonly IIndexedProductReader<IndexedProductDto> _indexedProductReader;
        private readonly IPriceSnapshotRepository _snapshotRepository;
        private readonly IAnalyticsUnitOfWork _unitOfWork;

        public AnalyticsService(
            IIndexedProductReader<IndexedProductDto> indexedProductReader,
            IPriceSnapshotRepository snapshotRepository,
            IAnalyticsUnitOfWork unitOfWork)
        {
            _indexedProductReader = indexedProductReader;
            _snapshotRepository = snapshotRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreatePriceSnapshotsAsync(Guid batchId, CancellationToken cancellationToken)
        {
            var products =  await _indexedProductReader.GetByBatchIdAsync(batchId,cancellationToken);

            if (products.Count == 0)
                return;

            var snapshots = products
                .Select(product => new PriceSnapshot(
                    batchId,
                    product.ProductKey,
                    product.ProductName,
                    product.Brand,
                    product.CategoryCode,
                    product.Source,
                    product.Price,
                    product.UnitPrice,
                    product.Currency,
                    product.CollectedAt))
                .ToList();

            await _snapshotRepository.AddRangeAsync(snapshots,cancellationToken);

            // save changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            
        }
    }
}
