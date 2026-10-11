using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceIndices.Service
{
    public interface ICategoryIndexService
    {
        Task<CategoryIndexDto?> CalculateAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<CategoryIndexDto>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);
    }
}
