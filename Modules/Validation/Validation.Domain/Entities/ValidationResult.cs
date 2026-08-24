namespace Validation.Domain.Entities
{
    public sealed class ValidationResult
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public Guid RawPriceId { get; private set; }

        public bool IsValid { get; private set; }

        public List<ValidationError> Errors { get; private set; } = [];

        public DateTime ValidatedAtUtc { get; private set; }

        private ValidationResult()
        {
        }

        public ValidationResult(
            Guid batchId,
            Guid rawPriceId,
            bool isValid,
            List<ValidationError> errors)
        {
            Id = Guid.NewGuid();
            BatchId = batchId;
            RawPriceId = rawPriceId;
            IsValid = isValid;
            Errors = errors;
            ValidatedAtUtc = DateTime.UtcNow;
        }
    }
}
