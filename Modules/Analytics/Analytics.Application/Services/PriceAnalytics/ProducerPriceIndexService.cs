using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Repository;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.PriceAnalytics
{

    public sealed class ProducerPriceIndexService
        : IProducerPriceIndexService
    {
        private readonly IProducerPriceIndexRepository _repository;

        private readonly IPpiWeightService _ppiWeightService;

        private readonly ILaspeyresIndexService
            _laspeyresIndexService;

        public ProducerPriceIndexService(
            IProducerPriceIndexRepository repository,
            IPpiWeightService ppiWeightService,
            ILaspeyresIndexService laspeyresIndexService)
        {
            _repository = repository;
            _ppiWeightService = ppiWeightService;
            _laspeyresIndexService = laspeyresIndexService;
        }

        public async Task<ProducerPriceIndexDto?>
            CalculateAsync(
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
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (existing is not null)
            {
                return MapToDto(existing);
            }

            var validation =
                await _ppiWeightService
                    .ValidateWeightSetAsync(
                        currentDate,
                        cancellationToken);

            if (!validation.IsValid)
            {
                throw new InvalidOperationException(
                    validation.ErrorMessage ??
                    "PPI weight set is invalid.");
            }

            var categoryWeights =
                await _ppiWeightService
                    .GetEffectiveWeightsAsync(
                        currentDate,
                        cancellationToken);

            if (categoryWeights.Count == 0)
                return null;

            decimal weightedIndex = 0m;

            foreach (var categoryWeight in categoryWeights)
            {
                var categoryIndex =
                    await _laspeyresIndexService
                        .CalculateAsync(
                            categoryWeight.CategoryCode,
                            currency,
                            baseDate,
                            currentDate,
                            cancellationToken);

                if (categoryIndex is null)
                    continue;

                weightedIndex +=
                    categoryIndex.IndexValue *
                    categoryWeight.Weight;
            }

            if (weightedIndex <= 0)
                return null;

            var baseBasketValue = 1m;

            var currentBasketValue =
                weightedIndex / 100m;

            var index = new ProducerPriceIndex(
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

        public async Task<
            IReadOnlyCollection<ProducerPriceIndexDto>>
            GetAsync(
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var indexes =
                await _repository.GetAsync(
                    currency,
                    from,
                    to,
                    cancellationToken);

            return indexes
                .Select(MapToDto)
                .ToList();
        }

        private static ProducerPriceIndexDto MapToDto(
            ProducerPriceIndex index)
        {
            return new ProducerPriceIndexDto
            {
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
