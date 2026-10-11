using Analytics.Application.DTOs;
using Analytics.Domain.Enums;

namespace Analytics.Application.Interfaces.Weights.Service
{
    public interface IPriceWeightService
    {
        Task<PriceWeightDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<PriceWeightDto?> GetEffectiveWeightAsync(
            string productKey,
            DateTime date,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceWeightDto>>
            GetEffectiveWeightsAsync(
                string? categoryCode,
                DateTime date,
                CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceWeightDto>>
            GetAsync(
                string? categoryCode,
                WeightType? weightType,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken);

        Task<PriceWeightDto> UpsertAsync(
            UpsertPriceWeightRequest request,
            CancellationToken cancellationToken);
        Task<WeightSetValidationResult> ValidateWeightSetAsync(
            string categoryCode,
            DateTime date,
            CancellationToken cancellationToken);
    }
}
