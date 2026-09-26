using Source.Application.Interfaces.Repository;
using Source.Infrastructure.Presistance.Data;

namespace Source.Infrastructure.Presistance.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SourceDbContext _context;
        public UnitOfWork(SourceDbContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
