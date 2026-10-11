namespace Analytics.Domain.Entities
{
    public sealed class CategoryIndex
    {
        public Guid Id { get; private set; }

        public string CategoryCode { get; private set; } = string.Empty;

        public string Currency { get; private set; } = string.Empty;

        public decimal BaseWeightedPrice { get; private set; }

        public decimal CurrentWeightedPrice { get; private set; }

        public decimal IndexValue { get; private set; }

        public decimal? ChangePercentage { get; private set; }

        public DateTime BaseDate { get; private set; }

        public DateTime CurrentDate { get; private set; }

        private CategoryIndex()
        {
        }

        public CategoryIndex(
            string categoryCode,
            string currency,
            decimal baseWeightedPrice,
            decimal currentWeightedPrice,
            DateTime baseDate,
            DateTime currentDate)
        {
            if (string.IsNullOrWhiteSpace(categoryCode))
                throw new ArgumentException(
                    "Category code is required.",
                    nameof(categoryCode));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException(
                    "Currency is required.",
                    nameof(currency));

            if (baseWeightedPrice <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(baseWeightedPrice),
                    "Base weighted price must be greater than zero.");

            if (currentWeightedPrice < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(currentWeightedPrice),
                    "Current weighted price cannot be negative.");

            Id = Guid.NewGuid();

            CategoryCode = categoryCode;
            Currency = currency;
            BaseWeightedPrice = baseWeightedPrice;
            CurrentWeightedPrice = currentWeightedPrice;
            BaseDate = baseDate;
            CurrentDate = currentDate;

            IndexValue =
                currentWeightedPrice /
                baseWeightedPrice *
                100m;

            ChangePercentage =
                IndexValue - 100m;
        }
    }
}
