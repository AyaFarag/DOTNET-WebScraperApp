using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.PriceIndices
{
    public sealed class PaascheIndexService
        : IPaascheIndexService
    {
        private readonly IPaascheIndexRepository _repository;
        private readonly IPriceWeightService _priceWeightService;
        private readonly IPriceSnapshotRepository _priceSnapshotRepository;

        public PaascheIndexService(
            IPaascheIndexRepository repository,
            IPriceWeightService priceWeightService,
            IPriceSnapshotRepository priceSnapshotRepository)
        {
            _repository = repository;
            _priceWeightService = priceWeightService;
            _priceSnapshotRepository = priceSnapshotRepository;
        }

        public async Task<PaascheIndexDto?> CalculateAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            if (baseDate >= currentDate)
                throw new ArgumentException(
                    "Base date must be earlier than current date.");

            var existing =
                await _repository.GetAsync(
                    categoryCode,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (existing is not null)
            {
                return MapToDto(existing);
            }

            // Paasche uses CURRENT-period weights.
            var weightValidation =
                await _priceWeightService.ValidateWeightSetAsync(
                    categoryCode,
                    currentDate,
                    cancellationToken);

            if (!weightValidation.IsValid)
            {
                throw new InvalidOperationException(
                    weightValidation.ErrorMessage ??
                    "Current-period weight set is invalid.");
            }

            var currentWeights =
                await _priceWeightService
                    .GetEffectiveWeightsAsync(
                        categoryCode,
                        currentDate,
                        cancellationToken);

            if (currentWeights.Count == 0)
                return null;

            decimal baseBasketValue = 0m;
            decimal currentBasketValue = 0m;

            foreach (var weight in currentWeights)
            {
                var baseSnapshot =
                    await _priceSnapshotRepository
                        .GetLatestAtOrBeforeAsync(
                            weight.ProductKey,
                            baseDate,
                            null,
                            currency,
                            cancellationToken);

                var currentSnapshot =
                    await _priceSnapshotRepository
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

                // CURRENT weights are used for BOTH periods.
                baseBasketValue +=
                    baseSnapshot.Price *
                    weight.Weight;

                currentBasketValue +=
                    currentSnapshot.Price *
                    weight.Weight;
            }

            if (baseBasketValue <= 0)
                return null;

            var index = new PaascheIndex(
                categoryCode,
                currency,
                baseBasketValue,
                currentBasketValue,
                baseDate,
                currentDate);

            await _repository.AddAsync(
                index,
                cancellationToken);

            return MapToDto(index);
        }

        public async Task<IReadOnlyCollection<PaascheIndexDto>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var indexes =
                await _repository.GetAsync(
                    categoryCode,
                    currency,
                    from,
                    to,
                    cancellationToken);

            return indexes
                .Select(MapToDto)
                .ToList();
        }

        private static PaascheIndexDto MapToDto(
            PaascheIndex index)
        {
            return new PaascheIndexDto
            {
                CategoryCode = index.CategoryCode,
                Currency = index.Currency,
                BaseBasketValue = index.BaseBasketValue,
                CurrentBasketValue = index.CurrentBasketValue,
                IndexValue = index.IndexValue,
                ChangePercentage = index.ChangePercentage,
                BaseDate = index.BaseDate,
                CurrentDate = index.CurrentDate
            };
        }
    }
}
