using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Anomalies.Service;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using MediatR;
using Shared.Contracts.Events.Analytics;
using Shared.Contracts.Events.Index;
using Shared.Contracts.Queries.Indexing;
using System.Text.Json;


namespace Analytics.Application.EventHandler
{
    public class PriceDataIndexedEventHandler : INotificationHandler<PriceDataIndexedEvent>
    {
        private readonly IIdempotencyService _idempotencyService;
        private readonly IOutboxRepository _outboxRepository;
        private readonly IIndexedProductReader<IndexedProductDto> _indexedProductReader;
        private readonly IAnomalyDetectionService _anomalyDetectionService;
        private readonly IAnalyticsService _analyticsService;
        private readonly IAnalyticsUnitOfWork _unitOfWork;
        public PriceDataIndexedEventHandler(IIdempotencyService idempotencyService,IOutboxRepository outboxRepository, IAnomalyDetectionService anomalyDetectionService, IAnalyticsService analyticsService, 
            IAnalyticsUnitOfWork unitOfWork, IIndexedProductReader<IndexedProductDto> indexedProductReader)
        {
            _idempotencyService = idempotencyService;
            _outboxRepository = outboxRepository;
            _anomalyDetectionService = anomalyDetectionService;
            _analyticsService = analyticsService;
            _unitOfWork = unitOfWork;
            _indexedProductReader = indexedProductReader;
        }
        public async Task Handle(PriceDataIndexedEvent notification, CancellationToken cancellationToken)
        {
            // 1. Check whether event was already processed
            var alreadyProcessed = await _idempotencyService.IsProcessedAsync(notification.EventId,
                    cancellationToken);

            if (alreadyProcessed)
            {
                return;
            }
      
            var execution = new AnalyticsExecution(notification.BatchId);

            await _analyticsService.CreatePriceSnapshotsAsync(notification.BatchId,cancellationToken); // including get index products reader , prepare snapshots , addRange snapshot to the db save changes

            await _anomalyDetectionService.DetectBatchAsync(notification.BatchId,cancellationToken); // include get snapshots by batch id , foreach them , in each interstion get historicalPrices , detect anomaly prices , prepare anomaly entity and save changes to db

            execution.Complete(productsProcessed: 0, snapshotsCreated: 0, anomaliesDetected: 0); // update execution status to completed and save changes to db

            var analyticsEvent = new AnalyticsCompletedEvent(
                Guid.NewGuid(),
                notification.BatchId
                );
            
            var outboxMessage = new OutboxMessage(analyticsEvent.EventId, nameof(AnalyticsCompletedEvent),
                 JsonSerializer.Serialize(analyticsEvent));

            await _outboxRepository.AddAsync(outboxMessage, cancellationToken);

            await _idempotencyService.MarkAsProcessedAsync(notification.EventId,
                nameof(AnalyticsCompletedEvent),
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Create execution
            //Update execution
            //Complete execution
        }
    }
}
