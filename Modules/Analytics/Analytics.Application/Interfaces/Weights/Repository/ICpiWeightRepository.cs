using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.Weights.Repository
{
    public interface ICpiWeightRepository
    {
        Task<CpiWeight?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<CpiWeight?> GetEffectiveWeightAsync(
            string categoryCode,
            DateTime date,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<CpiWeight>>
            GetEffectiveWeightsAsync(
                DateTime date,
                CancellationToken cancellationToken);

        Task<IReadOnlyCollection<CpiWeight>>
            GetAsync(
                string? categoryCode,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken);

        Task<bool> HasOverlappingWeightAsync(
            string categoryCode,
            DateTime effectiveFrom,
            DateTime? effectiveTo,
            Guid? excludeId,
            CancellationToken cancellationToken);

        Task AddAsync(
            CpiWeight weight,
            CancellationToken cancellationToken);

        void Update(CpiWeight weight);
    }
}
