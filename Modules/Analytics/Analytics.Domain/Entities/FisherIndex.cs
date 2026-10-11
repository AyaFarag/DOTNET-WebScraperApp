namespace Analytics.Domain.Entities
{
    public sealed class FisherIndex
    {
        public Guid Id { get; private set; }

        public string CategoryCode { get; private set; } = string.Empty;

        public string Currency { get; private set; } = string.Empty;

        public decimal LaspeyresValue { get; private set; }

        public decimal PaascheValue { get; private set; }

        public decimal IndexValue { get; private set; }

        public decimal? ChangePercentage { get; private set; }

        public DateTime BaseDate { get; private set; }

        public DateTime CurrentDate { get; private set; }

        private FisherIndex()
        {
        }

        public FisherIndex(
            string categoryCode,
            string currency,
            decimal laspeyresValue,
            decimal paascheValue,
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

            if (laspeyresValue < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(laspeyresValue),
                    "Laspeyres value cannot be negative.");

            if (paascheValue < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(paascheValue),
                    "Paasche value cannot be negative.");

            if (baseDate >= currentDate)
                throw new ArgumentException(
                    "Base date must be earlier than current date.");

            Id = Guid.NewGuid();

            CategoryCode = categoryCode;
            Currency = currency;

            LaspeyresValue = laspeyresValue;
            PaascheValue = paascheValue;

            BaseDate = baseDate;
            CurrentDate = currentDate;

            IndexValue =
                (decimal)Math.Sqrt(
                    (double)(laspeyresValue * paascheValue));

            ChangePercentage =
                IndexValue - 100m;
        }
    }
}
