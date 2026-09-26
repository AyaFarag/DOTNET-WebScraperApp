using Processing.Domain.Entities;

namespace Processing.Application.DTOs
{
    public sealed class ProcessingBatchResult
    {
        public Guid BatchId { get; } = Guid.NewGuid();

        public int TotalCount { get; }

        public int ProcessedCount { get; }

        public int FailedCount { get; }

        public IReadOnlyCollection<ProcessedProduct> ProcessedProducts { get; }

        public IReadOnlyCollection<ProcessingErrorResult> Errors { get; }

        public ProcessingBatchResult(Guid batchId, IReadOnlyCollection<ProcessedProduct> _ProcessedProducts)
        {
            BatchId = batchId;
            ProcessedProducts = _ProcessedProducts;
        }

        public ProcessingBatchResult(
            Guid batchId,
            int totalCount,
            int processedCount,
            int failedCount,
            IReadOnlyCollection<ProcessedProduct> _ProcessedProducts,
            IReadOnlyCollection<ProcessingErrorResult> errors)
        {
            BatchId = batchId;
            TotalCount = totalCount;
            ProcessedCount = processedCount;
            FailedCount = failedCount;
            ProcessedProducts = _ProcessedProducts;
            Errors = errors;
        }
    }
}
