using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceAnalytics.Service
{
    public interface IConsumerPriceIndexService
    {
        Task<ConsumerPriceIndexDto?> CalculateAsync(
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ConsumerPriceIndexDto>>
            GetAsync(
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);
    }
}
