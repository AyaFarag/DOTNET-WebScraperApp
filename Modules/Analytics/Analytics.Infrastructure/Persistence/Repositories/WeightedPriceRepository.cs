using Analytics.Application.Interfaces.Weights.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class WeightedPriceRepository
    : IWeightedPriceRepository
    {
        private readonly AnalyticsDbContext _context;

        public WeightedPriceRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<WeightedPrice?> GetAsync(
            string productKey,
            string? source,
            string currency,
            DateTime priceDate,
            CancellationToken cancellationToken)
        {
            var query = _context.WeightedPrices
                .Where(x =>
                    x.ProductKey == productKey &&
                    x.Currency == currency &&
                    x.PriceDate == priceDate);

            if (!string.IsNullOrWhiteSpace(source))
            {
                query = query.Where(
                    x => x.Source == source);
            }

            return await query
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<WeightedPrice>>
            GetAllAsync(
                string? categoryCode,
                string? source,
                string currency,
                DateTime priceDate,
                CancellationToken cancellationToken)
        {
            var query = _context.WeightedPrices
                .Where(x =>
                    x.Currency == currency &&
                    x.PriceDate == priceDate);

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                query = query.Where(
                    x => x.CategoryCode == categoryCode);
            }

            if (!string.IsNullOrWhiteSpace(source))
            {
                query = query.Where(
                    x => x.Source == source);
            }

            return await query
                .OrderBy(x => x.CategoryCode)
                .ThenBy(x => x.ProductKey)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            WeightedPrice weightedPrice,
            CancellationToken cancellationToken)
        {
            await _context.WeightedPrices.AddAsync(
                weightedPrice,
                cancellationToken);
        }
    }
}
