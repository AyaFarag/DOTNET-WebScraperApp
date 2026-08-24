using Processing.Domain.Entities;

namespace Processing.Application.DTOs
{
    public sealed class ProcessingBatchResult
    {
        public Guid BatchId { get; }

        public int TotalCount { get; }

        public int ProcessedCount { get; }

        public int FailedCount { get; }

        public IReadOnlyCollection<ProcessedPrice> ProcessedPrices { get; }

        public IReadOnlyCollection<ProcessingErrorResult> Errors { get; }

        public ProcessingBatchResult(Guid batchId, IReadOnlyCollection<ProcessedPrice> processedPrices)
        {
            BatchId = batchId;
            ProcessedPrices = processedPrices;
        }

        public ProcessingBatchResult(
            Guid batchId,
            int totalCount,
            int processedCount,
            int failedCount,
            IReadOnlyCollection<ProcessedPrice> processedPrices,
            IReadOnlyCollection<ProcessingErrorResult> errors)
        {
            BatchId = batchId;
            TotalCount = totalCount;
            ProcessedCount = processedCount;
            FailedCount = failedCount;
            ProcessedPrices = processedPrices;
            Errors = errors;
        }
    }
}
