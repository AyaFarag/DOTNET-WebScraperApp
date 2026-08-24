namespace Processing.Domain.Entities
{
    public sealed class ProcessedPrice
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public Guid RawPriceId { get; private set; }

        // Product
        public string ProductName { get; private set; } = null!;

        public string? NormalizedProductName { get; private set; }

        public string? Brand { get; private set; }

        // Price
        public decimal Price { get; private set; }

        public decimal? OriginalPrice { get; private set; }

        // Currency
        public string Currency { get; private set; } = null!;

        public string? NormalizedCurrency { get; private set; }

        // Quantity
        public decimal? Quantity { get; private set; }

        public decimal? NormalizedQuantity { get; private set; }

        // Unit
        public string? Unit { get; private set; }

        public string? NormalizedUnit { get; private set; }

        // Calculated
        public decimal? UnitPrice { get; private set; }

        public string Source { get; private set; } = null!;

        public decimal? PackageQuantity { get; private set; }

        public string? PackageUnit { get; private set; }

        public decimal? UnitsPerPackage { get; private set; }

        public decimal? TotalQuantity { get; private set; }

        public string? TotalUnit { get; private set; }

        public string? ProductKey { get; private set; }

        public DateTime ProcessedAtUtc { get; private set; }

        private ProcessedPrice()
        {
        }

        public ProcessedPrice(
            Guid batchId,
            Guid rawPriceId,
            string productName,
            decimal price,
            string currency,
            string source)
        {
            Id = Guid.NewGuid();

            BatchId = batchId;
            RawPriceId = rawPriceId;

            ProductName = productName;
            Price = price;
            Currency = currency;
            Source = source;

            ProcessedAtUtc = DateTime.UtcNow;
        }

        public void SetNormalizedProductName(
            string value)
        {
            NormalizedProductName = value;
        }

        public void SetBrand(
            string? value)
        {
            Brand = value;
        }

        public void SetNormalizedPrice(
            decimal value)
        {
            OriginalPrice = Price;
            Price = value;
        }

        public void SetNormalizedCurrency(
            string value)
        {
            NormalizedCurrency = value;
        }

        public void SetQuantity(
            decimal? quantity)
        {
            Quantity = quantity;
        }

        public void SetNormalizedQuantity(
            decimal? quantity)
        {
            NormalizedQuantity = quantity;
        }

        public void SetUnit(
            string? unit)
        {
            Unit = unit;
        }

        public void SetNormalizedUnit(
            string? unit)
        {
            NormalizedUnit = unit;
        }

        public void SetUnitPrice(
            decimal? unitPrice)
        {
            UnitPrice = unitPrice;
        }

        public void SetPackageInformation(
            decimal packageQuantity,
            string packageUnit,
            decimal unitsPerPackage,
            decimal totalQuantity,
            string totalUnit)
        {
            PackageQuantity = packageQuantity;
            PackageUnit = packageUnit;
            UnitsPerPackage = unitsPerPackage;
            TotalQuantity = totalQuantity;
            TotalUnit = totalUnit;
        }

        public void SetProductKey(string productKey)
        {
            ProductKey = productKey;
        }
    }
}
