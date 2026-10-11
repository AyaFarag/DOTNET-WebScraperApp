using Analytics.Application.Interfaces.Weights.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class CpiWeightRepository
        : ICpiWeightRepository
    {
        private readonly AnalyticsDbContext _context;

        public CpiWeightRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<CpiWeight?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.CpiWeights
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<CpiWeight?> GetEffectiveWeightAsync(
            string categoryCode,
            DateTime date,
            CancellationToken cancellationToken)
        {
            return await _context.CpiWeights
                .AsNoTracking()
                .Where(
                    x =>
                        x.CategoryCode == categoryCode &&
                        x.EffectiveFrom <= date &&
                        (!x.EffectiveTo.HasValue ||
                         x.EffectiveTo > date))
                .OrderByDescending(x => x.EffectiveFrom)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<CpiWeight>>
            GetEffectiveWeightsAsync(
                DateTime date,
                CancellationToken cancellationToken)
        {
            return await _context.CpiWeights
                .AsNoTracking()
                .Where(
                    x =>
                        x.EffectiveFrom <= date &&
                        (!x.EffectiveTo.HasValue ||
                         x.EffectiveTo > date))
                .OrderBy(x => x.CategoryCode)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<CpiWeight>>
            GetAsync(
                string? categoryCode,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken)
        {
            var query = _context.CpiWeights
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                query = query.Where(
                    x => x.CategoryCode == categoryCode);
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
                .ThenByDescending(x => x.EffectiveFrom)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasOverlappingWeightAsync(
            string categoryCode,
            DateTime effectiveFrom,
            DateTime? effectiveTo,
            Guid? excludeId,
            CancellationToken cancellationToken)
        {
            var query = _context.CpiWeights
                .Where(x => x.CategoryCode == categoryCode);

            if (excludeId.HasValue)
            {
                query = query.Where(
                    x => x.Id != excludeId.Value);
            }

            var newEnd =
                effectiveTo ?? DateTime.MaxValue;

            return await query.AnyAsync(
                x =>
                    x.EffectiveFrom < newEnd &&
                    (x.EffectiveTo == null ||
                     x.EffectiveTo > effectiveFrom),
                cancellationToken);
        }

        public async Task AddAsync(
            CpiWeight weight,
            CancellationToken cancellationToken)
        {
            await _context.CpiWeights.AddAsync(
                weight,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public void Update(CpiWeight weight)
        {
            _context.CpiWeights.Update(weight);
        }
    }
}
