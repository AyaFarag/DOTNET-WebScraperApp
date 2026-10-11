using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Application.Interfaces.Weights.Repository;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Entities;
using Analytics.Domain.Enums;

namespace Analytics.Application.Services.Weights
{
    public sealed class PriceWeightService
    : IPriceWeightService
    {
        private readonly IPriceWeightRepository _repository;
        private readonly IAnalyticsUnitOfWork _unitOfWork;

        public PriceWeightService(
            IPriceWeightRepository repository,
            IAnalyticsUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PriceWeightDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var weight =
                await _repository.GetByIdAsync(
                    id,
                    cancellationToken);

            return weight is null
                ? null
                : Map(weight);
        }

        public async Task<PriceWeightDto?> GetEffectiveWeightAsync(
            string productKey,
            DateTime date,
            CancellationToken cancellationToken)
        {
            var weight =
                await _repository.GetEffectiveWeightAsync(
                    productKey,
                    date,
                    cancellationToken);

            return weight is null
                ? null
                : Map(weight);
        }

        public async Task<IReadOnlyCollection<PriceWeightDto>>
            GetEffectiveWeightsAsync(
                string? categoryCode,
                DateTime date,
                CancellationToken cancellationToken)
        {
            var weights =
                await _repository.GetEffectiveWeightsAsync(
                    categoryCode,
                    date,
                    cancellationToken);

            return weights
                .Select(Map)
                .ToList();
        }

        public async Task<IReadOnlyCollection<PriceWeightDto>>
            GetAsync(
                string? categoryCode,
                WeightType? weightType,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken)
        {
            var weights =
                await _repository.GetAsync(
                    categoryCode,
                    weightType,
                    effectiveFrom,
                    effectiveTo,
                    cancellationToken);

            return weights
                .Select(Map)
                .ToList();
        }

        public async Task<PriceWeightDto> UpsertAsync(
            UpsertPriceWeightRequest request,
            CancellationToken cancellationToken)
        {
            var overlapping =
                await _repository.HasOverlappingWeightAsync(
                    request.ProductKey,
                    request.EffectiveFrom,
                    request.EffectiveTo,
                    null,
                    cancellationToken);

            if (overlapping)
            {
                throw new InvalidOperationException(
                    "An overlapping weight already exists for this product.");
            }

            var weight = new PriceWeight(
                request.ProductKey,
                request.ProductName,
                request.CategoryCode,
                request.WeightType,
                request.Weight,
                request.EffectiveFrom,
                request.EffectiveTo);

            await _repository.AddAsync(
                weight,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Map(weight);
        }

        private static PriceWeightDto Map(
            PriceWeight weight)
        {
            return new PriceWeightDto
            {
                Id = weight.Id,
                ProductKey = weight.ProductKey,
                ProductName = weight.ProductName,
                CategoryCode = weight.CategoryCode,
                WeightType = weight.WeightType,
                Weight = weight.Weight,
                WeightPercentage = weight.Weight * 100m,
                EffectiveFrom = weight.EffectiveFrom,
                EffectiveTo = weight.EffectiveTo
            };
        }

        public async Task<WeightSetValidationResult> ValidateWeightSetAsync(
        string categoryCode,
        DateTime date,
        CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(categoryCode))
            {
                throw new ArgumentException(
                    "Category code is required.",
                    nameof(categoryCode));
            }

            var weights = await _repository.GetEffectiveWeightsAsync(
                    categoryCode,
                    date,
                    cancellationToken);

            if (weights.Count == 0)
            {
                return new WeightSetValidationResult
                {
                    CategoryCode = categoryCode,
                    Date = date,
                    WeightCount = 0,
                    TotalWeight = 0,
                    TotalWeightPercentage = 0,
                    IsValid = false,
                    ErrorMessage =
                        "No effective weights were found for the category."
                };
            }

            var totalWeight = weights.Sum(x => x.Weight);

            // Allow a very small decimal precision difference.
            const decimal tolerance = 0.000001m;

            var isValid =
                Math.Abs(totalWeight - 1m) <= tolerance;

            return new WeightSetValidationResult
            {
                CategoryCode = categoryCode,
                Date = date,
                WeightCount = weights.Count,
                TotalWeight = totalWeight,
                TotalWeightPercentage = totalWeight * 100m,
                IsValid = isValid,
                ErrorMessage = isValid
                    ? null
                    : $"Weight total must equal 1.0 (100%). " +
                      $"Current total is {totalWeight:P6}."
            };
        }
    }
}
