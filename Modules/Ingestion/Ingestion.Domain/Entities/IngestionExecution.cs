namespace Ingestion.Domain.Entities;


public class IngestionExecution
{
    public Guid Id { get; private set; }

    public Guid BatchId { get; private set; }

    public string Source { get; private set; } = null!;

    public DateTime StartedAt { get; private set; }

    public DateTime? CompletedAt { get; private set; }

    public int RecordsCollected { get; private set; }

    public string Status { get; private set; } = null!;

    public string? ErrorMessage { get; private set; }

    private IngestionExecution()
    {
    }

    public IngestionExecution(
        Guid batchId,
        string source)
    {
        Id = Guid.NewGuid();

        BatchId = batchId;
        Source = source;

        StartedAt = DateTime.UtcNow;

        Status = "Running";
    }

    public void Complete(int recordsCollected)
    {
        RecordsCollected = recordsCollected;
        CompletedAt = DateTime.UtcNow;
        Status = "Completed";
    }

    public void Fail(string errorMessage)
    {
        ErrorMessage = errorMessage;
        CompletedAt = DateTime.UtcNow;
        Status = "Failed";
    }
}
