namespace Analytics.Domain.Entities
{
    public sealed class WeightedPrice
    {
        public Guid Id { get; private set; }

        public string ProductKey { get; private set; } = string.Empty;

        public string? ProductName { get; private set; }

        public string? CategoryCode { get; private set; }

        public string? Source { get; private set; }

        public string Currency { get; private set; } = string.Empty;

        public decimal Price { get; private set; }

        public decimal Weight { get; private set; }

        public decimal WeightedValue { get; private set; }

        public DateTime PriceDate { get; private set; }

        private WeightedPrice()
        {
        }

        public WeightedPrice(
            string productKey,
            string? productName,
            string? categoryCode,
            string? source,
            string currency,
            decimal price,
            decimal weight,
            DateTime priceDate)
        {
            if (string.IsNullOrWhiteSpace(productKey))
                throw new ArgumentException(
                    "Product key is required.",
                    nameof(productKey));

            if (price < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(price),
                    "Price cannot be negative.");

            if (weight < 0 || weight > 1)
                throw new ArgumentOutOfRangeException(
                    nameof(weight),
                    "Weight must be between 0 and 1.");

            Id = Guid.NewGuid();

            ProductKey = productKey;
            ProductName = productName;
            CategoryCode = categoryCode;
            Source = source;
            Currency = currency;
            Price = price;
            Weight = weight;
            PriceDate = priceDate;

            WeightedValue = price * weight;
        }
    }
}
