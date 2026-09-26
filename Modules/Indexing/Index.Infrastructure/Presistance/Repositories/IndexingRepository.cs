using Index.Application.Interfaces.Repository;
using Index.Domain.Entities;
using Index.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Index.Infrastructure.Presistance.Repositories
{
    public class IndexingRepository : IIndexingRepository
    {
        private readonly IndexingDbContext _context;
        public IndexingRepository(IndexingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(IndexedProduct entity, CancellationToken cancellationToken)
        {
            await _context.indexedProducts.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<IndexedProduct> entities, CancellationToken cancellationToken)
        {
            await _context.indexedProducts.AddRangeAsync(entities, cancellationToken);
        }

        public async Task<IReadOnlyList<IndexedProduct>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken)
        {
            return await _context.indexedProducts.Where(x=>x.BatchId == batchId).ToListAsync();
        }

        public async Task<IndexedProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.indexedProducts.FirstOrDefaultAsync(x=>x.Id == id);
        }
        public async Task<bool> ExistsAsync(Guid classificationResultId,CancellationToken cancellationToken)
        {
            return await _context.indexedProducts.AnyAsync(x => x.ClassificationResultId ==classificationResultId,cancellationToken);
        }
    }
}
