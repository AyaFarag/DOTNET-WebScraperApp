using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.PriceIndices
{
    public sealed class CategoryIndexService
    : ICategoryIndexService
    {
        private readonly IWeightedPriceService _weightedPriceService;
        private readonly ICategoryIndexRepository _repository;
        private readonly IAnalyticsUnitOfWork _unitOfWork;

        public CategoryIndexService(
            IWeightedPriceService weightedPriceService,
            ICategoryIndexRepository repository,
            IAnalyticsUnitOfWork unitOfWork)
        {
            _weightedPriceService = weightedPriceService;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryIndexDto?> CalculateAsync(
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

            var baseWeightedPrices =
                await _weightedPriceService.CalculateAsync(
                    categoryCode,
                    null,
                    currency,
                    baseDate,
                    cancellationToken);

            var currentWeightedPrices =
                await _weightedPriceService.CalculateAsync(
                    categoryCode,
                    null,
                    currency,
                    currentDate,
                    cancellationToken);

            //if (baseWeightedPrices.Count == 0 ||
            //    currentWeightedPrices.Count == 0)
            //{
            //    return null;
            //}

            //var baseTotal =
            //    baseWeightedPrices.Sum(
            //        x => x.WeightedValue);

            //var currentTotal =
            //    currentWeightedPrices.Sum(
            //        x => x.WeightedValue);

            //if (baseTotal <= 0)
            //    return null;

            //var categoryIndex =
            //    new CategoryIndex(
            //        categoryCode,
            //        currency,
            //        baseTotal,
            //        currentTotal,
            //        baseDate,
            //        currentDate);

            //await _repository.AddAsync(
            //    categoryIndex,
            //    cancellationToken);

            //await _unitOfWork.SaveChangesAsync(
            //    cancellationToken);

            //return Map(categoryIndex);
            return null;
        }

        public async Task<IReadOnlyCollection<CategoryIndexDto>>
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

        private static CategoryIndexDto Map(
            CategoryIndex entity)
        {
            return new CategoryIndexDto
            {
                CategoryCode = entity.CategoryCode,
                Currency = entity.Currency,
                BaseWeightedPrice =
                    entity.BaseWeightedPrice,
                CurrentWeightedPrice =
                    entity.CurrentWeightedPrice,
                IndexValue = entity.IndexValue,
                ChangePercentage =
                    entity.ChangePercentage,
                BaseDate = entity.BaseDate,
                CurrentDate = entity.CurrentDate
            };
        }
    }
}
