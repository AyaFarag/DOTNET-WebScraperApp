namespace Analytics.Domain.Entities
{
    public sealed class ConsumerPriceIndex
    {
        public Guid Id { get; private set; }

        public string Currency { get; private set; } = string.Empty;

        public decimal BaseBasketValue { get; private set; }

        public decimal CurrentBasketValue { get; private set; }

        public decimal IndexValue { get; private set; }

        public decimal? ChangePercentage { get; private set; }

        public DateTime BaseDate { get; private set; }

        public DateTime CurrentDate { get; private set; }

        private ConsumerPriceIndex()
        {
        }

        public ConsumerPriceIndex(
            string currency,
            decimal baseBasketValue,
            decimal currentBasketValue,
            DateTime baseDate,
            DateTime currentDate)
        {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException(
                    "Currency is required.",
                    nameof(currency));

            if (baseBasketValue <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(baseBasketValue),
                    "Base basket value must be greater than zero.");

            if (currentBasketValue < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(currentBasketValue),
                    "Current basket value cannot be negative.");

            if (baseDate >= currentDate)
                throw new ArgumentException(
                    "Base date must be earlier than current date.");

            Id = Guid.NewGuid();

            Currency = currency;

            BaseBasketValue = baseBasketValue;
            CurrentBasketValue = currentBasketValue;

            BaseDate = baseDate;
            CurrentDate = currentDate;

            IndexValue =
                currentBasketValue /
                baseBasketValue *
                100m;

            ChangePercentage =
                IndexValue - 100m;
        }
    }
}
