using Analytics.Application.DTOs;
using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.Repositories
{
    public interface IAnalyticsExecutionRepository
    {
        Task<PagedResult<AnalyticsExecution>> GetAsync(
            int page,
            int pageSize,
            string? status,
            CancellationToken cancellationToken);

        Task<AnalyticsExecution?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
