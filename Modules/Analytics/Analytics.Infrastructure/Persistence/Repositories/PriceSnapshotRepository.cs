using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class PriceSnapshotRepository: IPriceSnapshotRepository
    {
        private readonly AnalyticsDbContext _context;

        public PriceSnapshotRepository(AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IReadOnlyCollection<PriceSnapshot> snapshots,CancellationToken cancellationToken)
        {
            await _context.PriceSnapshots.AddRangeAsync(snapshots,cancellationToken);
        }

        public async Task<IReadOnlyCollection<PriceSnapshot>> GetByProductKeyAsync(string productKey, string? source, string? currency, CancellationToken cancellationToken)
        {
            var query = _context.PriceSnapshots.AsNoTracking().Where(x => x.ProductKey == productKey);

            if (source is not null)
            {
                query = query.Where(x => x.Source == source);
            }
            if (currency is not null)
            {
                query = query.Where(x => x.Currency == currency);
            }

            return await query.OrderBy(x => x.CollectedAt).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<PriceSnapshot>> GetForAnalyticsAsync(string? categoryCode, string? source, string? Currency, CancellationToken cancellationToken)
        {
            var query = _context.PriceSnapshots.AsNoTracking();

            if (categoryCode is not null)
                query = query.Where(x => x.CategoryCode == categoryCode);

            if (source is not null)
                query = query.Where(x => x.Source == source);

            if (Currency is not null)
            {
                query = query.Where(x => x.Currency == Currency);
            }

            return await query.
                OrderBy(x => x.ProductKey)
               .ThenBy(x => x.CollectedAt)
               .ToListAsync(cancellationToken);
        }


        public async Task<IReadOnlyCollection<PriceSnapshot>> GetHistoricalAsync(string productKey,string? source,string currency,DateTime before,
        CancellationToken cancellationToken)
        {
            IQueryable<PriceSnapshot> query =
                _context.PriceSnapshots
                    .Where(x =>
                        x.ProductKey == productKey &&
                        x.Currency == currency &&
                        x.CollectedAt < before);

            if (!string.IsNullOrWhiteSpace(source))
            {
                query = query.Where(
                    x => x.Source == source);
            }

            return await query
                .OrderByDescending(x => x.CollectedAt)
                .ToListAsync(cancellationToken);
        }


        public async Task<IReadOnlyCollection<PriceSnapshot>> GetByBatchIdAsync(
        Guid batchId,
        CancellationToken cancellationToken)
        {
            return await _context.PriceSnapshots
                .Where(x => x.BatchId == batchId)
                .OrderBy(x => x.ProductKey)
                .ThenBy(x => x.CollectedAt)
                .ToListAsync(cancellationToken);
        }

        public Task<PriceSnapshot?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(PriceSnapshot snapshot, CancellationToken cancellationToken)
        {
            await _context.PriceSnapshots.AddAsync(snapshot, cancellationToken);
        }

        public async Task<IReadOnlyCollection<PriceSnapshot>> GetProductHistoryAsync(
                string productKey,
                DateTime from,
                DateTime to,
                string? source,
                string? currency,
                CancellationToken cancellationToken)
        {
            IQueryable<PriceSnapshot> query = _context.PriceSnapshots
                .AsNoTracking()
                    .Where(x =>
                        x.ProductKey == productKey &&
                        x.CollectedAt >= from &&
                        x.CollectedAt <= to);

            if (!string.IsNullOrWhiteSpace(source))
            {
                query = query.Where(
                    x => x.Source == source);
            }
            
            if (!string.IsNullOrWhiteSpace(currency))
            {
                query = query.Where(
                    x => x.Currency == currency);
            }

            return await query
                .OrderBy(x => x.CollectedAt)
                .ToListAsync(cancellationToken);
        }


        public async Task<IReadOnlyCollection<PriceSnapshot>> GetLatestPricesByProductAsync(string productKey,string? currency, CancellationToken cancellationToken)
        {
            var snapshots = _context.PriceSnapshots
                .AsNoTracking()
                .Where(x =>
                    x.ProductKey == productKey);
            if (currency is not null) 
            {
                snapshots = snapshots.Where(x => x.Currency == currency);
            }

            return await snapshots
                .GroupBy(x => x.Source)
                .Select(g => g.OrderByDescending(x => x.CollectedAt).First())
                .ToListAsync(cancellationToken);


        }
    }
}
