using Analytics.Application.DTOs;
using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class PriceSnapshotReader : IPriceSnapshotReader
    {
        private readonly AnalyticsDbContext _context;

        public PriceSnapshotReader(AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<PriceSnapshotDto>> SearchAsync(PriceSnapshotSearchRequest request,
            CancellationToken cancellationToken)
        {
            var query = _context.PriceSnapshots
                    .AsNoTracking()
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.ProductKey))
            {
                query = query.Where(x =>
                    x.ProductKey == request.ProductKey);
            }

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                query = query.Where(x =>
                    x.ProductName != null &&
                    x.ProductName.Contains(
                        request.ProductName));
            }

            if (!string.IsNullOrWhiteSpace(request.Brand))
            {
                query = query.Where(x =>
                    x.Brand == request.Brand);
            }

            if (!string.IsNullOrWhiteSpace(request.CategoryCode))
            {
                query = query.Where(x =>
                    x.CategoryCode == request.CategoryCode);
            }

            if (!string.IsNullOrWhiteSpace(request.Source))
            {
                query = query.Where(x =>
                    x.Source == request.Source);
            }

            if (!string.IsNullOrWhiteSpace(request.Currency))
            {
                query = query.Where(x =>
                    x.Currency == request.Currency);
            }

            if (request.MinimumPrice.HasValue)
            {
                query = query.Where(x =>
                    x.Price >= request.MinimumPrice.Value);
            }

            if (request.MaximumPrice.HasValue)
            {
                query = query.Where(x =>
                    x.Price <= request.MaximumPrice.Value);
            }

            if (request.From.HasValue)
            {
                query = query.Where(x =>
                    x.CollectedAt >= request.From.Value);
            }

            if (request.To.HasValue)
            {
                query = query.Where(x =>
                    x.CollectedAt <= request.To.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                    .OrderByDescending(x => x.CollectedAt)
                    .Skip(
                        (request.Page - 1) *
                        request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new PriceSnapshotDto
                    {
                        Id = x.Id,
                        ProductKey = x.ProductKey,
                        ProductName = x.ProductName,
                        Brand = x.Brand,
                        CategoryCode = x.CategoryCode,
                        Source = x.Source,
                        Price = x.Price,
                        UnitPrice = x.UnitPrice,
                        Currency = x.Currency,
                        CollectedAt = x.CollectedAt
                    })
                    .ToListAsync(cancellationToken);

            return new PagedResult<PriceSnapshotDto>(items,request.Page, request.PageSize,totalCount);
        }
    }
}
