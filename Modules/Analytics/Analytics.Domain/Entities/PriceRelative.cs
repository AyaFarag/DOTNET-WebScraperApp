namespace Analytics.Domain.Entities
{
    public sealed class PriceRelative
    {
        public Guid Id { get; private set; }

        public string ProductKey { get; private set; } = string.Empty;

        public string? ProductName { get; private set; }

        public string? Brand { get; private set; }

        public string? CategoryCode { get; private set; }

        public string? Source { get; private set; }

        public string Currency { get; private set; } = string.Empty;

        public decimal BasePrice { get; private set; }

        public decimal CurrentPrice { get; private set; }

        public decimal RelativeValue { get; private set; }

        public DateTime BaseDate { get; private set; }

        public DateTime CurrentDate { get; private set; }

        private PriceRelative()
        {
        }

        public PriceRelative(
            string productKey,
            string? productName,
            string? brand,
            string? categoryCode,
            string? source,
            string currency,
            decimal basePrice,
            decimal currentPrice,
            DateTime baseDate,
            DateTime currentDate)
        {
            if (basePrice <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(basePrice),
                    "Base price must be greater than zero.");

            if (currentPrice < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(currentPrice),
                    "Current price cannot be negative.");

            Id = Guid.NewGuid();

            ProductKey = productKey;
            ProductName = productName;
            Brand = brand;
            CategoryCode = categoryCode;
            Source = source;
            Currency = currency;

            BasePrice = basePrice;
            CurrentPrice = currentPrice;

            BaseDate = baseDate;
            CurrentDate = currentDate;

            RelativeValue =
                currentPrice / basePrice * 100m;
        }
    }
}
