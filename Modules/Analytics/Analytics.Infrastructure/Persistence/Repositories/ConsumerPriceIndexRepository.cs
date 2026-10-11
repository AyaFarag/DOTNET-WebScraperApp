using Analytics.Application.Interfaces.PriceAnalytics.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class ConsumerPriceIndexRepository
        : IConsumerPriceIndexRepository
    {
        private readonly AnalyticsDbContext _context;

        public ConsumerPriceIndexRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<ConsumerPriceIndex?> GetAsync(
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            return await _context.ConsumerPriceIndexes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x =>
                        x.Currency == currency &&
                        x.BaseDate == baseDate &&
                        x.CurrentDate == currentDate,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<ConsumerPriceIndex>>
            GetAsync(
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var query = _context.ConsumerPriceIndexes
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
            ConsumerPriceIndex index,
            CancellationToken cancellationToken)
        {
            await _context.ConsumerPriceIndexes.AddAsync(
                index,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}
