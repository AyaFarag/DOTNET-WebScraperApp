namespace Validation.Domain.Entities
{
    public class ValidationExecution
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public string Status { get; private set; } = null!;

        public DateTime StartedAt { get; private set; }

        public DateTime? CompletedAt { get; private set; }

        public ValidationExecution(
            Guid batchId)
        {
            Id = Guid.NewGuid();
            BatchId = batchId;
            Status = "Processing";
            StartedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            Status = "Completed";
            CompletedAt = DateTime.UtcNow;
        }
    }
}
