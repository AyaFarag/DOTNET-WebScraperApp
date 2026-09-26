using Classification.Application.Interfaces;
using Classification.Application.Interfaces.Service;
using Classification.Domain.Enttities;
using MediatR;
using Shared.Contracts.Events.Classification;
using Shared.Contracts.Events.Processing;
using System.Text.Json;

namespace Classification.Application.EventHandler
{
    public sealed class PriceDataProcessedEventHandler : INotificationHandler<PriceDataProcessedEvent>
    {
        private readonly IIdempotencyService _idempotencyService;
        private readonly IOutboxRepository _outboxRepository;
        private readonly IClassificationService  _classificationService;
        private readonly IUnitOfWork _unitOfWork;

        public PriceDataProcessedEventHandler(IIdempotencyService idempotencyService, 
            IOutboxRepository outboxRepository, 
            IClassificationService classificationService,
            IUnitOfWork unitOfWork)
        {
            _idempotencyService = idempotencyService;
            _outboxRepository = outboxRepository;
            _classificationService = classificationService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(PriceDataProcessedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Check whether this event was already processed
                var alreadyProcessed = await _idempotencyService.IsProcessedAsync(notification.EventId, cancellationToken);

                if (alreadyProcessed)
                {
                    return;
                }

                // 2. Execute classification workflow
                await _classificationService.ClassifyBatchAsync(notification.BatchId, cancellationToken);


                // 3. Outbox pattern
                var @event = new PriceDataClassifiedEvent(Guid.NewGuid(), notification.BatchId);

                var outboxMessage = new OutboxMessage(typeof(PriceDataClassifiedEvent).AssemblyQualifiedName!,
                        JsonSerializer.Serialize(@event));

                await _outboxRepository.AddAsync(outboxMessage, cancellationToken);

                // 4. Mark event as processed
                await _idempotencyService.MarkAsProcessedAsync(notification.EventId,
                    nameof(PriceDataProcessedEvent), cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.ToString(), ex);
            }
        }
    }
}
