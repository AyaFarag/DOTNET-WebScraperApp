using Index.Application.Interfaces;
using Index.Application.Interfaces.Service;
using MediatR;
using Shared.Contracts.Events.Classification;

namespace Index.Application.EventHandler
{
    public sealed class PriceDataClassifiedEventHandler : INotificationHandler<PriceDataClassifiedEvent>
    {
        private readonly IIdempotencyService _idempotencyService;

        private readonly IIndexingService _indexingService;
        private readonly IIndexingUnitOfWork _unitOfWork;

        public PriceDataClassifiedEventHandler(
            IIdempotencyService idempotencyService,
            IIndexingService indexingService,
            IIndexingUnitOfWork unitOfWork)
        {
            _idempotencyService = idempotencyService;
            _indexingService = indexingService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(PriceDataClassifiedEvent notification,CancellationToken cancellationToken)
        {
            // 1. Check whether event was already processed
            var alreadyProcessed =  await _idempotencyService.IsProcessedAsync(notification.EventId,
                    cancellationToken);

            if (alreadyProcessed)
            {
                return;
            }

            // 2. Execute indexing
            await _indexingService.IndexBatchAsync(notification.BatchId,cancellationToken);

            // 3. Mark event as processed
            await _idempotencyService.MarkAsProcessedAsync(notification.EventId,
                nameof(PriceDataClassifiedEvent),
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
