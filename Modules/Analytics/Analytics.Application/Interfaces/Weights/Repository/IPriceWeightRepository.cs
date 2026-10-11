using Analytics.Domain.Entities;
using Analytics.Domain.Enums;

namespace Analytics.Application.Interfaces.Weights.Repository
{
    public interface IPriceWeightRepository
    {
        Task<PriceWeight?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<PriceWeight?> GetEffectiveWeightAsync(
            string productKey,
            DateTime date,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceWeight>> GetEffectiveWeightsAsync(
                string? categoryCode,
                DateTime date,
                CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceWeight>> GetAsync(
                string? categoryCode,
                WeightType? weightType,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken);

        Task<bool> HasOverlappingWeightAsync(
            string productKey,
            DateTime effectiveFrom,
            DateTime? effectiveTo,
            Guid? excludeId,
            CancellationToken cancellationToken);

        Task AddAsync(
            PriceWeight weight,
            CancellationToken cancellationToken);

        void Update(PriceWeight weight);
    }
}
