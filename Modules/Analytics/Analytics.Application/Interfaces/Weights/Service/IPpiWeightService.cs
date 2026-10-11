using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.Weights.Service
{

    public interface IPpiWeightService
    {
        Task<PpiWeightDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<PpiWeightDto?> GetEffectiveWeightAsync(
            string categoryCode,
            DateTime date,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PpiWeightDto>>
            GetEffectiveWeightsAsync(
            DateTime date,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PpiWeightDto>>
            GetAsync(
                string? categoryCode,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken);

        Task<PpiWeightDto> UpsertAsync(
            string categoryCode,
            string? categoryName,
            decimal weight,
            DateTime effectiveFrom,
            DateTime? effectiveTo,
            Guid? id,
            CancellationToken cancellationToken);

        Task<PpiWeightSetValidationResult>
            ValidateWeightSetAsync(
                DateTime date,
                CancellationToken cancellationToken);
    }
}
