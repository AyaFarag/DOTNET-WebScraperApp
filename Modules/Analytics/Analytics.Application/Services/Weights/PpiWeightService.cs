using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Weights.Repository;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.Weights
{
    public sealed class PpiWeightService
        : IPpiWeightService
    {
        private readonly IPpiWeightRepository _repository;

        public PpiWeightService(
            IPpiWeightRepository repository)
        {
            _repository = repository;
        }

        public async Task<PpiWeightDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var weight =
                await _repository.GetByIdAsync(
                    id,
                    cancellationToken);

            return weight is null
                ? null
                : MapToDto(weight);
        }

        public async Task<PpiWeightDto?>
            GetEffectiveWeightAsync(
                string categoryCode,
                DateTime date,
                CancellationToken cancellationToken)
        {
            var weight =
                await _repository.GetEffectiveWeightAsync(
                    categoryCode,
                    date,
                    cancellationToken);

            return weight is null
                ? null
                : MapToDto(weight);
        }

        public async Task<IReadOnlyCollection<PpiWeightDto>>
            GetEffectiveWeightsAsync(
                DateTime date,
                CancellationToken cancellationToken)
        {
            var weights =
                await _repository.GetEffectiveWeightsAsync(
                    date,
                    cancellationToken);

            return weights
                .Select(MapToDto)
                .ToList();
        }

        public async Task<IReadOnlyCollection<PpiWeightDto>>
            GetAsync(
                string? categoryCode,
                DateTime? effectiveFrom,
                DateTime? effectiveTo,
                CancellationToken cancellationToken)
        {
            var weights =
                await _repository.GetAsync(
                    categoryCode,
                    effectiveFrom,
                    effectiveTo,
                    cancellationToken);

            return weights
                .Select(MapToDto)
                .ToList();
        }

        public async Task<PpiWeightDto> UpsertAsync(
            string categoryCode,
            string? categoryName,
            decimal weight,
            DateTime effectiveFrom,
            DateTime? effectiveTo,
            Guid? id,
            CancellationToken cancellationToken)
        {
            var hasOverlap =
                await _repository.HasOverlappingWeightAsync(
                    categoryCode,
                    effectiveFrom,
                    effectiveTo,
                    id,
                    cancellationToken);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "The PPI weight period overlaps with an existing weight.");
            }

            if (id.HasValue)
            {
                var existing =
                    await _repository.GetByIdAsync(
                        id.Value,
                        cancellationToken);

                if (existing is null)
                    throw new KeyNotFoundException(
                        "PPI weight was not found.");

                existing.UpdateWeight(
                    weight,
                    effectiveFrom,
                    effectiveTo);

                _repository.Update(existing);

                return MapToDto(existing);
            }

            var entity = new PpiWeight(
                categoryCode,
                categoryName,
                weight,
                effectiveFrom,
                effectiveTo);

            await _repository.AddAsync(
                entity,
                cancellationToken);

            return MapToDto(entity);
        }

        public async Task<PpiWeightSetValidationResult>
            ValidateWeightSetAsync(
                DateTime date,
                CancellationToken cancellationToken)
        {
            var weights =
                await _repository.GetEffectiveWeightsAsync(
                    date,
                    cancellationToken);

            if (weights.Count == 0)
            {
                return new PpiWeightSetValidationResult
                {
                    Date = date,
                    WeightCount = 0,
                    TotalWeight = 0m,
                    TotalWeightPercentage = 0m,
                    IsValid = false,
                    ErrorMessage =
                        "No PPI weights are defined for the specified date."
                };
            }

            var totalWeight =
                weights.Sum(x => x.Weight);

            var tolerance = 0.000001m;

            var isValid =
                Math.Abs(totalWeight - 1m) <= tolerance;

            return new PpiWeightSetValidationResult
            {
                Date = date,
                WeightCount = weights.Count,
                TotalWeight = totalWeight,
                TotalWeightPercentage = totalWeight * 100m,
                IsValid = isValid,
                ErrorMessage = isValid
                    ? null
                    : "PPI category weights must sum to 100%."
            };
        }

        private static PpiWeightDto MapToDto(
            PpiWeight weight)
        {
            return new PpiWeightDto
            {
                Id = weight.Id,
                CategoryCode = weight.CategoryCode,
                CategoryName = weight.CategoryName,
                Weight = weight.Weight,
                WeightPercentage = weight.Weight * 100m,
                EffectiveFrom = weight.EffectiveFrom,
                EffectiveTo = weight.EffectiveTo
            };
        }
    }
}
