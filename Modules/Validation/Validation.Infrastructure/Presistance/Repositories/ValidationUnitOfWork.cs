using Validation.Application.Interfaces;
using Validation.Infrastructure.Presistance.Data;

namespace Validation.Infrastructure.Presistance.Repositories
{
    

    public class ValidationUnitOfWork : IUnitOfWork
    {
        private readonly ValidationDbContext _context;

        public ValidationUnitOfWork(ValidationDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
