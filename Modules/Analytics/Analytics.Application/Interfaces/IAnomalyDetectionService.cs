using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces
{
    public interface IAnomalyDetectionService
    {
        Task<AnomalyDetectionResult?> DetectAsync(Guid snapshotId, CancellationToken cancellationToken);
        Task DetectBatchAsync(Guid batchId,CancellationToken cancellationToken);
    }
}
