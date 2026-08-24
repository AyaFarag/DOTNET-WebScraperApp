using Validation.Domain.Entities;

namespace Validation.Application.DTOs
{
    public sealed class ValidationBatchResult
    {
        public Guid BatchId { get; }

        public IReadOnlyCollection<ValidationResult> Results { get; }

        public int TotalCount => Results.Count;

        public int ValidCount =>
            Results.Count(x => x.IsValid);

        public int InvalidCount =>
            Results.Count(x => !x.IsValid);

        public ValidationBatchResult(Guid batchId,IReadOnlyCollection<ValidationResult> results)
        {
            BatchId = batchId;
            Results = results;
        }
    }
}
