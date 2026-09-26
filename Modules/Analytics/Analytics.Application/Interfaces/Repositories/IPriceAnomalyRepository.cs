using Analytics.Application.DTOs;
using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.Repositories
{
    public interface IPriceAnomalyRepository
    {
        Task AddAsync(PriceAnomaly anomaly, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid snapshotId, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceAnomaly>> GetAsync(string? categoryCode,string? source, CancellationToken cancellationToken);

        Task<PagedResult<PriceAnomaly>> GetAsync(
            string? productKey,
            string? categoryCode,
            string? source,
            string? severity,
            string? direction,
            DateTime? from,
            DateTime? to,
            int page,
            int pageSize,
            CancellationToken cancellationToken);

        Task<PriceAnomaly?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
