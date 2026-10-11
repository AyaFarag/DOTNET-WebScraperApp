using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.Weights.Service
{
    public interface IWeightedPriceService
    {
        Task<WeightedPriceDto?> CalculateAsync(
            string productKey,
            string? source,
            string currency,
            DateTime date,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<WeightedPriceDto>>
            CalculateAllAsync(
                string categoryCode,
                string? source,
                string currency,
                DateTime date,
                CancellationToken cancellationToken);
    }
}
