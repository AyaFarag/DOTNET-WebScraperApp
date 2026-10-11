using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{

    public sealed class PriceRelativeRepository
        : IPriceRelativeRepository
    {
        private readonly AnalyticsDbContext _context;

        public PriceRelativeRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<PriceRelative?> GetAsync(
            string productKey,
            string? source,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            return await _context.PriceRelatives
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x =>
                        x.ProductKey == productKey &&
                        x.Source == source &&
                        x.Currency == currency &&
                        x.BaseDate == baseDate &&
                        x.CurrentDate == currentDate,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<PriceRelative>>
            GetAllAsync(
                string? categoryCode,
                string? source,
                string currency,
                DateTime baseDate,
                DateTime currentDate,
                CancellationToken cancellationToken)
        {
            return await _context.PriceRelatives
                .AsNoTracking()
                .Where(
                    x =>
                        x.Currency == currency &&
                        x.BaseDate == baseDate &&
                        x.CurrentDate == currentDate &&
                        (categoryCode == null ||
                         x.CategoryCode == categoryCode) &&
                        (source == null ||
                         x.Source == source))
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            PriceRelative priceRelative,
            CancellationToken cancellationToken)
        {
            await _context.PriceRelatives.AddAsync(
                priceRelative,
                cancellationToken);
        }
    }
}
