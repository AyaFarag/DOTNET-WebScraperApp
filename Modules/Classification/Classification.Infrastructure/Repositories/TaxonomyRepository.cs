using Classification.Application.Interfaces.Repository;
using Classification.Domain.Enttities;
using Classification.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Classification.Infrastructure.Repositories
{
    public sealed class TaxonomyRepository : ITaxonomyRepository
    {
        private readonly ClassificationDbContext _context;

        public TaxonomyRepository(ClassificationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsByCodeAsync(string code,CancellationToken cancellationToken)
        {
            return await _context.ClassificationCategories.AnyAsync( x => x.Code == code && x.IsActive,cancellationToken);
        }
        public async Task<IReadOnlyList<ClassificationCategory>> GetActiveCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.ClassificationCategories
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Level)
                .ThenBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<ClassificationCategory?> GetByCodeAsync(string code,CancellationToken cancellationToken)
        {
            return await _context.ClassificationCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Code == code &&
                         x.IsActive,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<ClassificationCategory>> GetChildrenAsync(Guid parentId, CancellationToken cancellationToken)
        {
            return await _context.ClassificationCategories
                .AsNoTracking()
                .Where(x =>
                    x.ParentId == parentId &&
                    x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
