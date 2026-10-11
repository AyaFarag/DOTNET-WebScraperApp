using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.PriceAnalytics.Repository
{
    public interface IProducerPriceIndexRepository
    {
        Task<ProducerPriceIndex?> GetAsync(
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ProducerPriceIndex>>
            GetAsync(
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);

        Task AddAsync(
            ProducerPriceIndex index,
            CancellationToken cancellationToken);
    }
}
