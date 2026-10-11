using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.PriceAnalytics.Repository
{
    public interface IConsumerPriceIndexRepository
    {
        Task<ConsumerPriceIndex?> GetAsync(
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ConsumerPriceIndex>>
            GetAsync(
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);

        Task AddAsync(
            ConsumerPriceIndex index,
            CancellationToken cancellationToken);
    }
}
