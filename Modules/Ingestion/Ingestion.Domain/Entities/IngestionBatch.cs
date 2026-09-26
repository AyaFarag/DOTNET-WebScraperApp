using Ingestion.Domain.Enums;

namespace Ingestion.Domain.Entities
{
    public class IngestionBatch
    {
        public Guid Id { get; private set; }

        public string Source { get; private set; } = null!;

        public BatchStatus Status { get; private set; }

        public int RecordsCollected { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime? CompletedAt { get; private set; }

        public string? ErrorMessage { get; private set; }

        private IngestionBatch()
        {
        }

        public IngestionBatch(string source)
        {
            Id = Guid.NewGuid();
            Source = source;
            Status = BatchStatus.Created;
            StartedAt = DateTime.UtcNow;
        }

        public void MarkScraping()
        {
            Status = BatchStatus.Scraping;
        }

        public void Complete(int recordsCount)
        {
            Status = BatchStatus.Completed;
            RecordsCollected = recordsCount;
            CompletedAt = DateTime.UtcNow;
        }

        public void Fail(string errorMessage)
        {
            Status = BatchStatus.Failed;
            ErrorMessage = errorMessage;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
