using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Repository;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Services.PriceAnalytics
{
    public sealed class ConsumerPriceIndexService
        : IConsumerPriceIndexService
    {
        private readonly IConsumerPriceIndexRepository _repository;

        private readonly ICpiWeightService _cpiWeightService;

        private readonly ILaspeyresIndexService
            _laspeyresIndexService;

        public ConsumerPriceIndexService(
            IConsumerPriceIndexRepository repository,
            ICpiWeightService cpiWeightService,
            ILaspeyresIndexService laspeyresIndexService)
        {
            _repository = repository;
            _cpiWeightService = cpiWeightService;
            _laspeyresIndexService = laspeyresIndexService;
        }

        public async Task<ConsumerPriceIndexDto?>
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
                await _cpiWeightService
                    .ValidateWeightSetAsync(
                        currentDate,
                        cancellationToken);

            if (!validation.IsValid)
            {
                throw new InvalidOperationException(
                    validation.ErrorMessage ??
                    "CPI weight set is invalid.");
            }

            var categoryWeights =
                await _cpiWeightService
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
                {
                    continue;
                }

                weightedIndex +=
                    categoryIndex.IndexValue *
                    categoryWeight.Weight;
            }

            if (weightedIndex <= 0)
                return null;

            /*
             * CPI is normalized around 100.
             *
             * We represent the base basket as 1
             * and the current basket as CPI / 100.
             *
             * This keeps the resulting entity consistent
             * with the other index entities.
             */
            var baseBasketValue = 1m;

            var currentBasketValue =
                weightedIndex / 100m;

            var index = new ConsumerPriceIndex(
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

        public async Task<IReadOnlyCollection<ConsumerPriceIndexDto>>
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

        private static ConsumerPriceIndexDto MapToDto(
            ConsumerPriceIndex index)
        {
            return new ConsumerPriceIndexDto
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
