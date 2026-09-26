namespace Classification.Domain.Enttities
{
    public sealed class ClassificationExecution
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime? CompletedAt { get; private set; }

        public string Status { get; private set; } = null!;

        public int TotalProducts { get; private set; }
        public int ClassifiedProducts { get; private set; }
        public string? ModelName { get; private set; }
        public string? PromptVersion { get; private set; }

        public int FailedProducts { get; private set; }

        public string? ErrorMessage { get; private set; }

        private ClassificationExecution()
        {
        }

        public ClassificationExecution(
            Guid batchId,
            int totalProducts)
        {
            Id = Guid.NewGuid();

            BatchId = batchId;

            StartedAt = DateTime.UtcNow;

            Status = "Running";

            TotalProducts = totalProducts;

            ClassifiedProducts = 0;

            FailedProducts = 0;
        }

        public void IncrementClassified()
        {
            ClassifiedProducts++;
        }

        public void IncrementFailed()
        {
            FailedProducts++;
        }

        public void Complete()
        {
            Status = "Completed";

            CompletedAt = DateTime.UtcNow;
        }

        public void Fail(string errorMessage)
        {
            Status = "Failed";

            ErrorMessage = errorMessage;

            CompletedAt = DateTime.UtcNow;
        }
    }
}
