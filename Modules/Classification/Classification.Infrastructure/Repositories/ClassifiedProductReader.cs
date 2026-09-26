using Classification.Domain.Enttities;
using Classification.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Queries.Classification;

namespace Classification.Infrastructure.Repositories
{
    public class ClassifiedProductReader<TSource> : IClassifiedProductReader<ClassificationResult>
    {
        private readonly ClassificationDbContext _context;
        public ClassifiedProductReader(ClassificationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ClassificationResult>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken)
        {
            return await _context.ClassificationResults.Where(x=>x.BatchId == batchId).ToListAsync();
        }
    }
}
