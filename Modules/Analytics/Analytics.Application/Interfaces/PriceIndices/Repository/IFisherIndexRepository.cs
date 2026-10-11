using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.PriceIndices.Repository
{
    public interface IFisherIndexRepository
    {
        Task<FisherIndex?> GetAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<FisherIndex>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);

        Task AddAsync(
            FisherIndex index,
            CancellationToken cancellationToken);
    }
}
