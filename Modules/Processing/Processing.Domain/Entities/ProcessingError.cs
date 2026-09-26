namespace Processing.Domain.Entities
{
    public sealed class ProcessingError
    {
        public Guid Id { get; private set; }

        public Guid ProcessedProductId { get; private set; }

        public string Step { get; private set; } = null!;

        public string Message { get; private set; } = null!;

        public DateTime CreatedAtUtc { get; private set; }

        public ProcessedProduct ProcessedProduct { get; private set; } = null!;

        private ProcessingError()
        {
        }

        public ProcessingError(
            Guid ProcessedProductId,
            string step,
            string message)
        {
            Id = Guid.NewGuid();
            ProcessedProductId = ProcessedProductId;
            Step = step;
            Message = message;
            CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
