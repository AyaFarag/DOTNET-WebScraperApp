using Analytics.Application.Interfaces.Weights.Repository;
using Analytics.Domain.Entities;
using Analytics.Domain.Enums;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class PriceWeightRepository
    : IPriceWeightRepository
    {
        private readonly AnalyticsDbContext _context;

        public PriceWeightRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<PriceWeight?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.PriceWeights
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<PriceWeight?> GetEffectiveWeightAsync(
            string productKey,
            DateTime date,
            CancellationToken cancellationToken)
        {
            return await _context.PriceWeights
                .Where(x =>
                    x.ProductKey == productKey &&
                    x.EffectiveFrom <= date &&
                    (
                        x.EffectiveTo == null ||
                        x.EffectiveTo > date
                    ))
                .OrderByDescending(x => x.EffectiveFrom)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<PriceWeight>>
            GetEffectiveWeightsAsync(
                string? categoryCode,
                DateTime date,
                CancellationToken cancellationToken)
        {
            var query = _context.PriceWeights
                .Where(x =>
                    x.EffectiveFrom <= date &&
                    (
                        x.EffectiveTo == null ||
                        x.EffectiveTo > date
                    ));

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                query = query.Where(
                    x => x.CategoryCode == categoryCode);
            }

            return await query
                .OrderBy(x => x.CategoryCode)
                .ThenBy(x => x.ProductKey)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<PriceWeight>>
            GetAsync(
                string? categoryCode,
                WeightType? weightType,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken)
        {
            var query = _context.PriceWeights.AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                query = query.Where(
                    x => x.CategoryCode == categoryCode);
            }

            if (weightType.HasValue)
            {
                query = query.Where(
                    x => x.WeightType == weightType.Value);
            }

            if (effectiveFrom.HasValue)
            {
                query = query.Where(
                    x => x.EffectiveFrom >= effectiveFrom.Value);
            }

            if (effectiveTo.HasValue)
            {
                query = query.Where(
                    x =>
                        x.EffectiveTo == null ||
                        x.EffectiveTo <= effectiveTo.Value);
            }

            return await query
                .OrderBy(x => x.CategoryCode)
                .ThenBy(x => x.ProductKey)
                .ThenBy(x => x.EffectiveFrom)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasOverlappingWeightAsync(
            string productKey,
            DateTime effectiveFrom,
            DateTime? effectiveTo,
            Guid? excludeId,
            CancellationToken cancellationToken)
        {
            var query = _context.PriceWeights
                .Where(x => x.ProductKey == productKey);

            if (excludeId.HasValue)
            {
                query = query.Where(
                    x => x.Id != excludeId.Value);
            }

            return await query.AnyAsync(
                x =>
                    x.EffectiveFrom <
                        (effectiveTo ?? DateTime.MaxValue) &&
                    (x.EffectiveTo == null ||
                     x.EffectiveTo > effectiveFrom),
                cancellationToken);
        }

        public async Task AddAsync(
            PriceWeight weight,
            CancellationToken cancellationToken)
        {
            await _context.PriceWeights.AddAsync(
                weight,
                cancellationToken);
        }

        public void Update(PriceWeight weight)
        {
            _context.PriceWeights.Update(weight);
        }
    }
}
