namespace Analytics.Domain.Entities
{
    public sealed class AnalyticsExecution
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public string Status { get; private set; } = string.Empty;

        public DateTime StartedAt { get; private set; }

        public DateTime? CompletedAt { get; private set; }

        public int ProductsProcessed { get; private set; }

        public int SnapshotsCreated { get; private set; }

        public int AnomaliesDetected { get; private set; }

        public string? ErrorMessage { get; private set; }

        private AnalyticsExecution()
        {
        }

        public AnalyticsExecution(Guid batchId)
        {
            Id = Guid.NewGuid();
            BatchId = batchId;
            Status = "Running";
            StartedAt = DateTime.UtcNow;
        }

        public void Complete(
            int productsProcessed,
            int snapshotsCreated,
            int anomaliesDetected)
        {
            Status = "Completed";
            CompletedAt = DateTime.UtcNow;

            ProductsProcessed = productsProcessed;
            SnapshotsCreated = snapshotsCreated;
            AnomaliesDetected = anomaliesDetected;
        }

        public void Fail(string errorMessage)
        {
            Status = "Failed";
            CompletedAt = DateTime.UtcNow;
            ErrorMessage = errorMessage;
        }
    }
}
