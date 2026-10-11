using Analytics.Application.Interfaces.Weights.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class PpiWeightRepository
        : IPpiWeightRepository
    {
        private readonly AnalyticsDbContext _context;

        public PpiWeightRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<PpiWeight?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.PpiWeights
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<PpiWeight?> GetEffectiveWeightAsync(
            string categoryCode,
            DateTime date,
            CancellationToken cancellationToken)
        {
            return await _context.PpiWeights
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

        public async Task<IReadOnlyCollection<PpiWeight>>
            GetEffectiveWeightsAsync(
                DateTime date,
                CancellationToken cancellationToken)
        {
            return await _context.PpiWeights
                .AsNoTracking()
                .Where(
                    x =>
                        x.EffectiveFrom <= date &&
                        (!x.EffectiveTo.HasValue ||
                         x.EffectiveTo > date))
                .OrderBy(x => x.CategoryCode)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<PpiWeight>>
            GetAsync(
                string? categoryCode,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken)
        {
            var query = _context.PpiWeights
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
            var query = _context.PpiWeights
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
            PpiWeight weight,
            CancellationToken cancellationToken)
        {
            await _context.PpiWeights.AddAsync(
                weight,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public void Update(PpiWeight weight)
        {
            _context.PpiWeights.Update(weight);
        }
    }
}
