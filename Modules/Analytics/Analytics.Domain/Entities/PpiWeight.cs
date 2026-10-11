namespace Analytics.Domain.Entities
{
    public sealed class PpiWeight
    {
        public Guid Id { get; private set; }

        public string CategoryCode { get; private set; } = string.Empty;

        public string? CategoryName { get; private set; }

        public decimal Weight { get; private set; }

        public DateTime EffectiveFrom { get; private set; }

        public DateTime? EffectiveTo { get; private set; }

        private PpiWeight()
        {
        }

        public PpiWeight(
            string categoryCode,
            string? categoryName,
            decimal weight,
            DateTime effectiveFrom,
            DateTime? effectiveTo)
        {
            if (string.IsNullOrWhiteSpace(categoryCode))
                throw new ArgumentException(
                    "Category code is required.",
                    nameof(categoryCode));

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

            CategoryCode = categoryCode;
            CategoryName = categoryName;
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
