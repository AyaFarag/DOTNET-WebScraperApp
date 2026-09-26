using Classification.Application.Interfaces;
using Classification.Infrastructure.Persistence.Data;

namespace Classification.Infrastructure.Repositories
{
    public class ProcessingUnitOfWork : IUnitOfWork
    {
        private readonly ClassificationDbContext _context;
        public ProcessingUnitOfWork(ClassificationDbContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
