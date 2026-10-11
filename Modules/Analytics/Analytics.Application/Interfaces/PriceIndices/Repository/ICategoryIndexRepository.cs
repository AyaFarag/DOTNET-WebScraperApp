using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.PriceIndices.Repository
{
    public interface ICategoryIndexRepository
    {
        Task<CategoryIndex?> GetAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<CategoryIndex>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);

        Task AddAsync(
            CategoryIndex categoryIndex,
            CancellationToken cancellationToken);
    }
}
