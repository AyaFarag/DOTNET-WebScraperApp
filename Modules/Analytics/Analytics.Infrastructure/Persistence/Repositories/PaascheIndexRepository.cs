using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class PaascheIndexRepository
        : IPaascheIndexRepository
    {
        private readonly AnalyticsDbContext _context;

        public PaascheIndexRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<PaascheIndex?> GetAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            return await _context.PaascheIndexes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x =>
                        x.CategoryCode == categoryCode &&
                        x.Currency == currency &&
                        x.BaseDate == baseDate &&
                        x.CurrentDate == currentDate,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<PaascheIndex>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var query = _context.PaascheIndexes
                .AsNoTracking()
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
                .OrderByDescending(x => x.CurrentDate)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            PaascheIndex index,
            CancellationToken cancellationToken)
        {
            await _context.PaascheIndexes.AddAsync(
                index,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}
