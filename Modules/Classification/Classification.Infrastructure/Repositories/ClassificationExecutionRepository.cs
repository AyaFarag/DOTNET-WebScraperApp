using Classification.Application.Interfaces.Repository;
using Classification.Domain.Enttities;
using Classification.Infrastructure.Persistence.Data;

namespace Classification.Infrastructure.Repositories
{
    public sealed class ClassificationExecutionRepository : IClassificationExecutionRepository
    {
        private readonly ClassificationDbContext _context;

        public ClassificationExecutionRepository(ClassificationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClassificationExecution execution,CancellationToken cancellationToken)
        {
            await _context.ClassificationExecutions.AddAsync(execution,cancellationToken);
        }
    }
}
