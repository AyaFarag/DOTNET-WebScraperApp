using Analytics.Domain.Enums;

namespace Analytics.Application.DTOs
{
    public sealed record UpsertPriceWeightRequest
    {
        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? CategoryCode { get; init; }

        public WeightType WeightType { get; init; }

        public decimal Weight { get; init; }

        public DateTime EffectiveFrom { get; init; }

        public DateTime? EffectiveTo { get; init; }
    }
}
