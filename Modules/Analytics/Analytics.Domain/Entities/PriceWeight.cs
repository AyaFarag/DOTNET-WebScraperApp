using Analytics.Domain.Enums;

namespace Analytics.Domain.Entities
{
    public sealed class PriceWeight
    {
        public Guid Id { get; private set; }

        public string ProductKey { get; private set; } = string.Empty;

        public string? ProductName { get; private set; }

        public string? CategoryCode { get; private set; }

        public WeightType WeightType { get; private set; }

        /// <summary>
        /// Normalized weight between 0 and 1.
        /// Example: 0.25 = 25%.
        /// </summary>
        public decimal Weight { get; private set; }

        public DateTime EffectiveFrom { get; private set; }

        public DateTime? EffectiveTo { get; private set; }

        private PriceWeight()
        {
        }

        public PriceWeight(
            string productKey,
            string? productName,
            string? categoryCode,
            WeightType weightType,
            decimal weight,
            DateTime effectiveFrom,
            DateTime? effectiveTo)
        {
            if (string.IsNullOrWhiteSpace(productKey))
                throw new ArgumentException(
                    "Product key is required.",
                    nameof(productKey));

            if (weight < 0 || weight > 1)
                throw new ArgumentOutOfRangeException(
                    nameof(weight),
                    "Weight must be between 0 and 1.");

            if (effectiveTo.HasValue &&
                effectiveTo.Value <= effectiveFrom)
            {
                throw new ArgumentException(
                    "EffectiveTo must be greater than EffectiveFrom.");
            }

            Id = Guid.NewGuid();

            ProductKey = productKey;
            ProductName = productName;
            CategoryCode = categoryCode;
            WeightType = weightType;
            Weight = weight;
            EffectiveFrom = effectiveFrom;
            EffectiveTo = effectiveTo;
        }

        public void UpdateWeight(
            decimal weight,
            DateTime effectiveFrom,
            DateTime? effectiveTo)
        {
            if (weight < 0 || weight > 1)
                throw new ArgumentOutOfRangeException(
                    nameof(weight),
                    "Weight must be between 0 and 1.");

            if (effectiveTo.HasValue &&
                effectiveTo.Value <= effectiveFrom)
            {
                throw new ArgumentException(
                    "EffectiveTo must be greater than EffectiveFrom.");
            }

            Weight = weight;
            EffectiveFrom = effectiveFrom;
            EffectiveTo = effectiveTo;
        }
    }
}
