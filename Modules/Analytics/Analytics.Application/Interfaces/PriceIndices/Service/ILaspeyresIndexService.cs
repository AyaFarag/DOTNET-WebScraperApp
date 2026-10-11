using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceIndices.Service
{
    public interface ILaspeyresIndexService
    {
        Task<LaspeyresIndexDto?> CalculateAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<LaspeyresIndexDto>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);
    }
}
