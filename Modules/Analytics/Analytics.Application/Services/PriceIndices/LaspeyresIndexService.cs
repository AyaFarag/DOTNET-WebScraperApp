using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Application.Interfaces.Weights.Repository;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.PriceIndices
{
    public sealed class LaspeyresIndexService
    : ILaspeyresIndexService
    {
        private readonly IPriceWeightRepository _weightRepository;
        private readonly IPriceSnapshotRepository _snapshotRepository;
        private readonly ILaspeyresIndexRepository _repository;
        private readonly IPriceWeightService _weightService;
        private readonly IAnalyticsUnitOfWork _unitOfWork;

        public LaspeyresIndexService(
            IPriceWeightRepository weightRepository,
            IPriceSnapshotRepository snapshotRepository,
            ILaspeyresIndexRepository repository,
            IPriceWeightService weightService,
            IAnalyticsUnitOfWork unitOfWork)
        {
            _weightRepository = weightRepository;
            _snapshotRepository = snapshotRepository;
            _repository = repository;
            _weightService = weightService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LaspeyresIndexDto?> CalculateAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            if (baseDate >= currentDate)
            {
                throw new ArgumentException(
                    "Base date must be earlier than current date.");
            }

            var existing =
                await _repository.GetAsync(
                    categoryCode,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (existing is not null)
                return Map(existing);

            var validation =
                await _weightService.ValidateWeightSetAsync(
                    categoryCode,
                    baseDate,
                    cancellationToken);

            if (!validation.IsValid)
            {
                throw new InvalidOperationException(
                    validation.ErrorMessage ??
                    "The base-period weight set is invalid.");
            }

            var baseWeights =
                await _weightRepository
                    .GetEffectiveWeightsAsync(
                        categoryCode,
                        baseDate,
                        cancellationToken);

            if (baseWeights.Count == 0)
                return null;

            decimal baseBasketValue = 0m;
            decimal currentBasketValue = 0m;

            foreach (var weight in baseWeights)
            {
                var baseSnapshot =
                    await _snapshotRepository
                        .GetLatestAtOrBeforeAsync(
                            weight.ProductKey,
                            baseDate,
                            null,
                            currency,
                            cancellationToken);

                var currentSnapshot =
                    await _snapshotRepository
                        .GetLatestAtOrBeforeAsync(
                            weight.ProductKey,
                            currentDate,
                            null,
                            currency,
                            cancellationToken);

                if (baseSnapshot is null ||
                    currentSnapshot is null)
                {
                    continue;
                }

                baseBasketValue +=
                    baseSnapshot.Price * weight.Weight;

                currentBasketValue +=
                    currentSnapshot.Price * weight.Weight;
            }

            if (baseBasketValue <= 0)
                return null;

            var index =
                new LaspeyresIndex(
                    categoryCode,
                    currency,
                    baseBasketValue,
                    currentBasketValue,
                    baseDate,
                    currentDate);

            await _repository.AddAsync(
                index,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Map(index);
        }

        public async Task<IReadOnlyCollection<LaspeyresIndexDto>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var results =
                await _repository.GetAsync(
                    categoryCode,
                    currency,
                    from,
                    to,
                    cancellationToken);

            return results
                .Select(Map)
                .ToList();
        }

        private static LaspeyresIndexDto Map(
            LaspeyresIndex entity)
        {
            return new LaspeyresIndexDto
            {
                CategoryCode = entity.CategoryCode,
                Currency = entity.Currency,
                BaseBasketValue = entity.BaseBasketValue,
                CurrentBasketValue = entity.CurrentBasketValue,
                IndexValue = entity.IndexValue,
                ChangePercentage = entity.ChangePercentage,
                BaseDate = entity.BaseDate,
                CurrentDate = entity.CurrentDate
            };
        }
    }
}
