using Index.Application.Interfaces;
using Index.Infrastructure.Presistance.Data;

namespace Index.Infrastructure.Presistance.Repositories
{
    public class IndexingUnitOfWork : IIndexingUnitOfWork
    {
        private readonly IndexingDbContext _context;
        public IndexingUnitOfWork(IndexingDbContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
