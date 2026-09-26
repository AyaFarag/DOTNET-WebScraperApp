using Microsoft.EntityFrameworkCore;
using Processing.Domain.Entities;
using Processing.Infrastructure.Presistance.Data;
using Shared.Contracts.Queries.Processing;

namespace Processing.Infrastructure.Presistance.Repository
{
    public class ProcessedProductReader<T> : IProcessedProductReader<ProcessedProduct>
    {
        private readonly ProcessingDbContext _context;
        public ProcessedProductReader(ProcessingDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IReadOnlyList<ProcessedProduct>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken)
        {
            var processedProducts = await _context.ProcessedPrices
                .Where(p => p.BatchId == batchId)
                .ToListAsync(cancellationToken);
            return processedProducts;
        }
    }
}
