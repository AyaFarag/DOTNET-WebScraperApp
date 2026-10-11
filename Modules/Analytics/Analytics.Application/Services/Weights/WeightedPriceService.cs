using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Application.Interfaces.Weights.Repository;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.Weights
{
    public sealed class WeightedPriceService
    : IWeightedPriceService
    {
        private readonly IPriceSnapshotRepository _snapshotRepository;
        private readonly IPriceWeightRepository _weightRepository;
        private readonly IWeightedPriceRepository _weightedPriceRepository;
        private readonly IPriceWeightService _weightService;
        private readonly IAnalyticsUnitOfWork _unitOfWork;

        public WeightedPriceService(
            IPriceSnapshotRepository snapshotRepository,
            IPriceWeightRepository weightRepository,
            IWeightedPriceRepository weightedPriceRepository,
            IPriceWeightService weightService,
            IAnalyticsUnitOfWork unitOfWork)
        {
            _snapshotRepository = snapshotRepository;
            _weightRepository = weightRepository;
            _weightedPriceRepository = weightedPriceRepository;
            _weightService = weightService;
            _unitOfWork = unitOfWork;
        }

        public async Task<WeightedPriceDto?> CalculateAsync(
            string productKey,
            string? source,
            string currency,
            DateTime date,
            CancellationToken cancellationToken)
        {
            var weight =
                await _weightRepository.GetEffectiveWeightAsync(
                    productKey,
                    date,
                    cancellationToken);

            if (weight is null)
                return null;

            var snapshot =
                await _snapshotRepository.GetLatestAtOrBeforeAsync(
                    productKey,
                    date,
                    source,
                    currency,
                    cancellationToken);

            if (snapshot is null)
                return null;

            var weightedPrice = new WeightedPrice(
                snapshot.ProductKey,
                snapshot.ProductName,
                snapshot.CategoryCode,
                snapshot.Source,
                snapshot.Currency,
                snapshot.Price,
                weight.Weight,
                snapshot.CollectedAt);

            await _weightedPriceRepository.AddAsync(
                weightedPrice,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Map(weightedPrice);
        }

        public async Task<IReadOnlyCollection<WeightedPriceDto>>
            CalculateAllAsync(
                string categoryCode,
                string? source,
                string currency,
                DateTime date,
                CancellationToken cancellationToken)
        {
            var validation =
                await _weightService.ValidateWeightSetAsync(
                    categoryCode,
                    date,
                    cancellationToken);

            if (!validation.IsValid)
            {
                throw new InvalidOperationException(
                    validation.ErrorMessage ??
                    "The weight set is invalid.");
            }

            var weights =
                await _weightRepository.GetEffectiveWeightsAsync(
                    categoryCode,
                    date,
                    cancellationToken);

            var results = new List<WeightedPriceDto>();

            foreach (var weight in weights)
            {
                var snapshot =
                await _snapshotRepository
                        .GetLatestAtOrBeforeAsync(
                            weight.ProductKey,
                            date,
                            source,
                            currency,
                            cancellationToken);

                if (snapshot is null)
                    continue;

                var weightedPrice = new WeightedPrice(
                    snapshot.ProductKey,
                    snapshot.ProductName,
                    snapshot.CategoryCode,
                    snapshot.Source,
                    snapshot.Currency,
                    snapshot.Price,
                    weight.Weight,
                    snapshot.CollectedAt);

                await _weightedPriceRepository.AddAsync(
                    weightedPrice,
                    cancellationToken);

                results.Add(Map(weightedPrice));
            }

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return results;
        }

        private static WeightedPriceDto Map(
            WeightedPrice weightedPrice)
        {
            return new WeightedPriceDto
            {
                ProductKey = weightedPrice.ProductKey,
                ProductName = weightedPrice.ProductName,
                CategoryCode = weightedPrice.CategoryCode,
                Source = weightedPrice.Source,
                Currency = weightedPrice.Currency,
                Price = weightedPrice.Price,
                Weight = weightedPrice.Weight,
                WeightPercentage =
                    weightedPrice.Weight * 100m,
                WeightedValue =
                    weightedPrice.WeightedValue,
                PriceDate =
                    weightedPrice.PriceDate
            };
        }
    }
}
