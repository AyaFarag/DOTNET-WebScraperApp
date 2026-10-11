using Analytics.Application.Events;
using Analytics.Application.Interfaces.Anomalies.Repository;
using Analytics.Application.Interfaces.Anomalies.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using MediatR;

namespace Analytics.Application.Services.Anomalies
{
    public sealed class AnomalyDetectionService : IAnomalyDetectionService
    {
        private readonly IPriceSnapshotRepository _snapshotRepository;
        private readonly IPriceAnomalyRepository _anomalyRepository;
        private readonly IAnomalyDetector _anomalyDetector;
        private readonly IAnalyticsUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;
        public AnomalyDetectionService(
            IPriceSnapshotRepository snapshotRepository,
            IPriceAnomalyRepository anomalyRepository,
            IAnomalyDetector anomalyDetector,
            IAnalyticsUnitOfWork unitOfWork,
            IPublisher publisher)
        {
            _snapshotRepository = snapshotRepository;
            _anomalyRepository = anomalyRepository;
            _anomalyDetector = anomalyDetector;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task DetectAsync(Guid snapshotId,CancellationToken cancellationToken)
        {
            // 1. Get current snapshot
            var snapshot =  await _snapshotRepository.GetByIdAsync(snapshotId,cancellationToken);

            if (snapshot is null)
            {
                return;
            }

            // 2. Idempotency check
            var alreadyDetected = await _anomalyRepository.ExistsAsync(snapshot.Id,cancellationToken);

            if (alreadyDetected)
            {
                return;
            }

            // 3. Get historical observations
            var historicalSnapshots = await _snapshotRepository.GetHistoricalAsync(
                    snapshot.ProductKey,
                    snapshot.Source,
                    snapshot.Currency,
                    snapshot.CollectedAt,
                    cancellationToken);

            // 4. Extract historical prices
            var historicalPrices =  historicalSnapshots.Select(x => x.Price).ToList();

            // 5. Not enough data
            if (historicalPrices.Count < 4)
            {
                return;
            }

            // 6. Run mathematical detector
            var result = _anomalyDetector.Detect(snapshot.Price,historicalPrices);

            // 7. Normal price → nothing to persist
            if (!result.IsAnomaly)
            {
                return;
            }

            // 8. Create anomaly entity
            var anomaly = new PriceAnomaly(
                snapshot.Id,
                snapshot.ProductKey,
                snapshot.ProductName,
                snapshot.Brand,
                snapshot.CategoryCode,
                snapshot.Source,
                snapshot.Currency,
                snapshot.Price,
                result);

            // 9. Persist anomaly
            await _anomalyRepository.AddAsync(anomaly, cancellationToken);

            // 7. Publish event
            await _publisher.Publish(
                new AnomalyDetectedEvent(
                    Guid.NewGuid(),
                    anomaly.Id),
                cancellationToken);

            // save changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DetectBatchAsync(Guid batchId,CancellationToken cancellationToken)
        {
            // 1. Get snapshots from this batch
            var snapshots =  await _snapshotRepository.GetByBatchIdAsync(batchId,cancellationToken);

            // 2. Detect anomaly for each snapshot
            foreach (var snapshot in snapshots)
            {
                await DetectAsync(snapshot.Id,cancellationToken);
            }
        }




        Task<AnomalyDetectionResult?> IAnomalyDetectionService.DetectAsync(Guid snapshotId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
