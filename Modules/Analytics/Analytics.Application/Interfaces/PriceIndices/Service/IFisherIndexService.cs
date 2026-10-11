using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceIndices.Service
{
    public interface IFisherIndexService
    {
        Task<FisherIndexDto?> CalculateAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<FisherIndexDto>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);
    }
}
