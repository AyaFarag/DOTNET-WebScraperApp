using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceIndices.Service
{
    public interface IPaascheIndexService
    {
        Task<PaascheIndexDto?> CalculateAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PaascheIndexDto>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);
    }
}
