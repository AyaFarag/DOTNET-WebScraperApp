using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class PriceAnomalyRepository : IPriceAnomalyRepository
    {
        private readonly AnalyticsDbContext _context;

        public PriceAnomalyRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PriceAnomaly anomaly,CancellationToken cancellationToken)
        {
            await _context.PriceAnomalies.AddAsync(anomaly,cancellationToken);

        }

        public Task<bool> ExistsAsync(Guid snapshotId,CancellationToken cancellationToken)
        {
            return _context.PriceAnomalies.AnyAsync(x => x.SnapshotId == snapshotId, cancellationToken);
        }

        public async Task<IReadOnlyCollection<PriceAnomaly>> GetAsync(string? categoryCode,string? source,CancellationToken cancellationToken)
        {
            IQueryable<PriceAnomaly> query = _context.PriceAnomalies;

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                query = query.Where(x => x.CategoryCode == categoryCode);
            }

            if (!string.IsNullOrWhiteSpace(source))
            {
                query = query.Where(x => x.Source == source);
            }

            return await query
                .OrderByDescending(x => x.DetectedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<PriceAnomaly>> GetAsync(
            string? productKey,
            string? categoryCode,
            string? source,
            string? severity,
            string? direction,
            DateTime? from,
            DateTime? to,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<PriceAnomaly> query = _context.PriceAnomalies.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(productKey))
                query = query.Where(x =>x.ProductKey == productKey);

            if (!string.IsNullOrWhiteSpace(categoryCode))
                query = query.Where(x => x.CategoryCode == categoryCode);

            if (!string.IsNullOrWhiteSpace(source))
                query = query.Where(x =>x.Source == source);

            if (!string.IsNullOrWhiteSpace(severity))
                query = query.Where(x => x.Severity == severity);

            if (!string.IsNullOrWhiteSpace(direction))
                query = query.Where(x => x.Direction == direction);

            if (from.HasValue)
                query = query.Where(x => x.DetectedAt >= from.Value);

            if (to.HasValue)
                query = query.Where(x =>x.DetectedAt <= to.Value);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(x => x.DetectedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<PriceAnomaly>(items,page, pageSize, totalCount);
        }

        public async Task<PriceAnomaly?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
        {
            return await _context.PriceAnomalies
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }
    }
}
