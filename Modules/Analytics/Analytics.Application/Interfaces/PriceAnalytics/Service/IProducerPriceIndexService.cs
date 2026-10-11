using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceAnalytics.Service
{
    public interface IProducerPriceIndexService
    {
        Task<ProducerPriceIndexDto?> CalculateAsync(
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ProducerPriceIndexDto>>
            GetAsync(
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);
    }
}
