using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories
{
    public sealed class AnalyticsExecutionRepository
    : IAnalyticsExecutionRepository
    {
        private readonly AnalyticsDbContext _context;

        public AnalyticsExecutionRepository(
            AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<AnalyticsExecution>> GetAsync(
            int page,
            int pageSize,
            string? status,
            CancellationToken cancellationToken)
        {
            var query = _context.AnalyticsExecutions
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(x => x.Status == status);
            }

            var totalCount = await query.CountAsync(
                cancellationToken);

            var items = await query
                .OrderByDescending(x => x.StartedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<AnalyticsExecution>(
                items,
                page,
                pageSize,
                totalCount);
        }

        public async Task<AnalyticsExecution?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.AnalyticsExecutions
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }
    }
}
