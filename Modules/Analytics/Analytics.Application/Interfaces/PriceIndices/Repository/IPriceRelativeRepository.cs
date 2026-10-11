using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.PriceIndices.Repository
{
    public interface IPriceRelativeRepository
    {
        Task<PriceRelative?> GetAsync(
            string productKey,
            string? source,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceRelative>> GetAllAsync(
                string? categoryCode,
                string? source,
                string currency,
                DateTime baseDate,
                DateTime currentDate,
                CancellationToken cancellationToken);

        Task AddAsync(
            PriceRelative priceRelative,
            CancellationToken cancellationToken);
    }
}
