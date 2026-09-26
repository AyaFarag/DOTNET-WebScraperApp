using Index.Application.Interfaces.Repository;
using Index.Domain.Entities;
using Index.Infrastructure.Presistance.Data;

namespace Index.Infrastructure.Presistance.Repositories
{
    public class IndexingExecutionRepository : IIndexingExecutionRepository
    {
        private readonly IndexingDbContext _context;

        public IndexingExecutionRepository(IndexingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(IndexingExecution execution, CancellationToken cancellationToken)
        {
            await _context.indexingExecutions.AddAsync(execution, cancellationToken);
        }
    }
}
