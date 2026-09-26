using Processing.Application.Interfaces.Repositories;
using Processing.Infrastructure.Presistance.Data;

namespace Processing.Infrastructure.Presistance.Repository
{
    public class ProcessingUnitOfWork : IUnitOfWork
    {
        private readonly ProcessingDbContext _context;
        public ProcessingUnitOfWork(ProcessingDbContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
