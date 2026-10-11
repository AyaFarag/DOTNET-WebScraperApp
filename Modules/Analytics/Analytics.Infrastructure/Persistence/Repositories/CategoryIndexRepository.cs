using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class CategoryIndexRepository
    : ICategoryIndexRepository
    {
        private readonly AnalyticsDbContext _context;

        public CategoryIndexRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryIndex?> GetAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            return await _context.CategoryIndexes
                .FirstOrDefaultAsync(
                    x =>
                        x.CategoryCode == categoryCode &&
                        x.Currency == currency &&
                        x.BaseDate == baseDate &&
                        x.CurrentDate == currentDate,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<CategoryIndex>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var query = _context.CategoryIndexes
                .Where(x => x.Currency == currency);

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                query = query.Where(
                    x => x.CategoryCode == categoryCode);
            }

            if (from.HasValue)
            {
                query = query.Where(
                    x => x.CurrentDate >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(
                    x => x.CurrentDate <= to.Value);
            }

            return await query
                .OrderBy(x => x.CategoryCode)
                .ThenBy(x => x.CurrentDate)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            CategoryIndex categoryIndex,
            CancellationToken cancellationToken)
        {
            await _context.CategoryIndexes.AddAsync(
                categoryIndex,
                cancellationToken);
        }
    }
}
