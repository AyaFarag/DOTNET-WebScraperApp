using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class FisherIndexRepository
        : IFisherIndexRepository
    {
        private readonly AnalyticsDbContext _context;

        public FisherIndexRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<FisherIndex?> GetAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            return await _context.FisherIndexes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x =>
                        x.CategoryCode == categoryCode &&
                        x.Currency == currency &&
                        x.BaseDate == baseDate &&
                        x.CurrentDate == currentDate,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<FisherIndex>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var query = _context.FisherIndexes
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
            FisherIndex index,
            CancellationToken cancellationToken)
        {
            await _context.FisherIndexes.AddAsync(
                index,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}
