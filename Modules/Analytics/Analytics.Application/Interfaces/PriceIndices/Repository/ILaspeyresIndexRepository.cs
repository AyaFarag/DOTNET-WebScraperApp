using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.PriceIndices.Repository
{
    public interface ILaspeyresIndexRepository
    {
        Task<LaspeyresIndex?> GetAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<LaspeyresIndex>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);

        Task AddAsync(
            LaspeyresIndex index,
            CancellationToken cancellationToken);
    }
}
