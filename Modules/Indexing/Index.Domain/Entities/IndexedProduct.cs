namespace Index.Domain.Entities
{
    public sealed class IndexedProduct
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public Guid ClassificationResultId { get; private set; }

        public Guid ProcessedPriceId { get; private set; }

        public string ProductKey { get; private set; } = null!;

        public string ProductName { get; private set; } = null!;

        public string? Brand { get; private set; }

        public string CategoryCode { get; private set; } = null!;

        public string SubCategoryCode { get; private set; } = null!;

        public string ProductGroupCode { get; private set; } = null!;

        public string ProductTypeCode { get; private set; } = null!;

        public decimal Price { get; private set; }

        public string Currency { get; private set; } = null!;

        public decimal? UnitPrice { get; private set; }

        public string? Unit { get; private set; }

        public string Source { get; private set; } = null!;

        public DateTime CollectedAt { get; private set; }

        public DateTime IndexedAt { get; private set; }

        private IndexedProduct()
        {
        }

        public IndexedProduct(
            Guid batchId,
            Guid classificationResultId,
            Guid processedPriceId,
            string productKey,
            string productName,
            string? brand,
            string categoryCode,
            string subCategoryCode,
            string productGroupCode,
            string productTypeCode,
            decimal price,
            string currency,
            decimal? unitPrice,
            string? unit,
            string source,
            DateTime collectedAt)
        {
            Id = Guid.NewGuid();

            BatchId = batchId;

            ClassificationResultId =
                classificationResultId;

            ProcessedPriceId =
                processedPriceId;

            ProductKey = productKey;

            ProductName = productName;

            Brand = brand;

            CategoryCode = categoryCode;

            SubCategoryCode = subCategoryCode;

            ProductGroupCode = productGroupCode;

            ProductTypeCode = productTypeCode;

            Price = price;

            Currency = currency;

            UnitPrice = unitPrice;

            Unit = unit;

            Source = source;

            CollectedAt = collectedAt;

            IndexedAt = DateTime.UtcNow;
        }
    }
}
