using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceIndices.Service
{
    public interface IPriceRelativeService
    {
        Task<PriceRelativeDto?> CalculateAsync(
            string productKey,
            string? source,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceRelativeDto>>
            CalculateAllAsync(
                string? categoryCode,
                string? source,
                string currency,
                DateTime baseDate,
                DateTime currentDate,
                CancellationToken cancellationToken);
    }
}
