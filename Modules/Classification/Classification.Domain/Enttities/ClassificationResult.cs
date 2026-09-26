namespace Classification.Domain.Enttities
{
    public sealed class ClassificationResult
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public Guid ProcessedPriceId { get; private set; }
        public Guid ClassificationExecutionId { get; private set; }

        public string ProductKey { get; private set; } = null!;

        public string CategoryCode { get; private set; } = null!;

        public string SubCategoryCode { get; private set; } = null!;

        public string ProductGroupCode { get; private set; } = null!;

        public string ProductTypeCode { get; private set; } = null!;

        public double Confidence { get; private set; }

        public string Reason { get; private set; } = null!;

        public string ClassificationMethod { get; private set; } = null!;

        public DateTime CreatedAt { get; private set; }

        private ClassificationResult()
        {
        }

        public ClassificationResult(
        Guid batchId,
        Guid classificationExecutionId,
        Guid processedPriceId,
        string productKey,
        string categoryCode,
        string subCategoryCode,
        string productGroupCode,
        string productTypeCode,
        double confidence,
        string reason,
        string classificationMethod)
        {
            Id = Guid.NewGuid();

            BatchId = batchId;

            ClassificationExecutionId = classificationExecutionId;

            ProcessedPriceId = processedPriceId;

            ProductKey = productKey;

            CategoryCode = categoryCode;

            SubCategoryCode = subCategoryCode;

            ProductGroupCode = productGroupCode;

            ProductTypeCode = productTypeCode;

            Confidence = confidence;

            Reason = reason;

            ClassificationMethod = classificationMethod;

            CreatedAt = DateTime.UtcNow;
        }
    }
}
