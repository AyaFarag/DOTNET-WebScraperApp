using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Data;

namespace Infrastructure.Persistence.Repositories
{

    public class IngestionUnitOfWork : IUnitOfWork
    {
        private readonly IngestionDbContext _context;

        public IngestionUnitOfWork(IngestionDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(
            CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}
