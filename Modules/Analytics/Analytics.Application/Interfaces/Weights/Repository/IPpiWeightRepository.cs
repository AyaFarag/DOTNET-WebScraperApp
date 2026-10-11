using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.Weights.Repository
{
    public interface IPpiWeightRepository
    {
        Task<PpiWeight?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<PpiWeight?> GetEffectiveWeightAsync(
            string categoryCode,
            DateTime date,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PpiWeight>>
            GetEffectiveWeightsAsync(
                DateTime date,
                CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PpiWeight>>
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
            PpiWeight weight,
            CancellationToken cancellationToken);

        void Update(PpiWeight weight);
    }
}
