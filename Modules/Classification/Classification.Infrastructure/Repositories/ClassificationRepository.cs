using Classification.Application.Interfaces.Repository;
using Classification.Domain.Enttities;
using Classification.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Classification.Infrastructure.Repositories
{
    public class ClassificationRepository : IClassificationRepository
    {
        public readonly ClassificationDbContext _context;
        public ClassificationRepository(ClassificationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClassificationResult entity, CancellationToken cancellationToken)
        {
            await _context.ClassificationResults.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<ClassificationResult> entities, CancellationToken cancellationToken)
        {
            await _context.ClassificationResults.AddRangeAsync(entities, cancellationToken);
        }

        public async Task<IReadOnlyList<ClassificationResult>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken)
        {
            return await _context.ClassificationResults.Where(c => c.BatchId == batchId).ToListAsync(cancellationToken);
        }

        public async Task<ClassificationResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ClassificationResults.FindAsync(id, cancellationToken);
        }
    }
}

       
