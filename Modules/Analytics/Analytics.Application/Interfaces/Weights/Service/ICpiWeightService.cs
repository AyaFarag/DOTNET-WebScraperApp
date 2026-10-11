using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.Weights.Service
{
    public interface ICpiWeightService
    {
        Task<CpiWeightDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<CpiWeightDto?> GetEffectiveWeightAsync(
            string categoryCode,
            DateTime date,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<CpiWeightDto>>
            GetEffectiveWeightsAsync(
                DateTime date,
                CancellationToken cancellationToken);

        Task<IReadOnlyCollection<CpiWeightDto>>
            GetAsync(
                string? categoryCode,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken);

        Task<CpiWeightDto> UpsertAsync(
            string categoryCode,
            string? categoryName,
            decimal weight,
            DateTime effectiveFrom,
            DateTime? effectiveTo,
            Guid? id,
            CancellationToken cancellationToken);

        Task<CpiWeightSetValidationResult>
            ValidateWeightSetAsync(
                DateTime date,
                CancellationToken cancellationToken);
    }
}
