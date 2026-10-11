using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.Weights.Repository
{
    public interface IWeightedPriceRepository
    {
        Task<WeightedPrice?> GetAsync(
            string productKey,
            string? source,
            string currency,
            DateTime priceDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<WeightedPrice>>
            GetAllAsync(
                string? categoryCode,
                string? source,
                string currency,
                DateTime priceDate,
                CancellationToken cancellationToken);

        Task AddAsync(
            WeightedPrice weightedPrice,
            CancellationToken cancellationToken);
    }
}
