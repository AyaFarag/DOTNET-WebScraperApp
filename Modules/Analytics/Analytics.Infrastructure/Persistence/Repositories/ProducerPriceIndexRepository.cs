using Analytics.Application.Interfaces.PriceAnalytics.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class ProducerPriceIndexRepository
        : IProducerPriceIndexRepository
    {
        private readonly AnalyticsDbContext _context;

        public ProducerPriceIndexRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<ProducerPriceIndex?> GetAsync(
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            return await _context.ProducerPriceIndexes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x =>
                        x.Currency == currency &&
                        x.BaseDate == baseDate &&
                        x.CurrentDate == currentDate,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<ProducerPriceIndex>>
            GetAsync(
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var query = _context.ProducerPriceIndexes
                .AsNoTracking()
                .Where(x => x.Currency == currency);

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
            ProducerPriceIndex index,
            CancellationToken cancellationToken)
        {
            await _context.ProducerPriceIndexes.AddAsync(
                index,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}
