using MediatR;
using Shared.Contracts.Events.Ingestion;
using Shared.Contracts.Events.Validation;
using Shared.Contracts.Queries.Ingestion;
using Shared.Domain;
using System.Text.Json;
using Validation.Application.Interfaces;
using Validation.Application.Interfaces.Repository;
using Validation.Application.Interfaces.Services;
using Validation.Domain.Entities;

namespace Validation.Application.EventHandlers
{
    public sealed class PriceDataCollectedEventHandler : INotificationHandler<PriceDataCollectedEvent>
    {
        private readonly IIdempotencyService _idempotencyService;
        private readonly IValidationService _validationService;
        private readonly IRawPriceQueryReader<RawPrice> _rawPriceRepository;
        private readonly IValidationResultRepository _validationResultRepository;
        private readonly IOutboxRepository _outboxRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PriceDataCollectedEventHandler(
            IIdempotencyService idempotencyService,
            IValidationService validationService,
            IRawPriceQueryReader<RawPrice> rawPriceRepository,
            IValidationResultRepository validationResultRepository,
            IOutboxRepository outboxRepository,
            IUnitOfWork unitOfWork)
        {
            _idempotencyService = idempotencyService;
            _validationService = validationService;
            _rawPriceRepository = rawPriceRepository;
            _validationResultRepository = validationResultRepository;
            _outboxRepository = outboxRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(PriceDataCollectedEvent notification,CancellationToken cancellationToken)
        {
            try
            {
                // 1. Check idempotency
                var alreadyProcessed = await _idempotencyService.IsProcessedAsync(notification.EventId, cancellationToken);

                if (alreadyProcessed)
                {
                    return;
                }

                // 2. Get data using BatchId

                var rawPrices = await _rawPriceRepository.GetByBatchIdAsync(notification.BatchId, cancellationToken);

                // 3. Validate

                //await _validationService.ValidateAsync(notification.BatchId,rawPrices,cancellationToken);
                var validation = await _validationService.ValidateAsync(notification.BatchId, rawPrices, cancellationToken);

                // 4. Save validation results

                await _validationResultRepository.AddRangeAsync(validation.Results, cancellationToken);

                // 5. Create next event

                var validatedEvent =
                    new PriceDataValidatedEvent(
                        Guid.NewGuid(),
                        notification.BatchId,
                        validation.TotalCount,
                        validation.ValidCount,
                        validation.InvalidCount);

                // 6. Save event to Outbox

                var outboxMessage = new OutboxMessage(validatedEvent.EventId, nameof(PriceDataValidatedEvent),
                    JsonSerializer.Serialize(validatedEvent));

                await _outboxRepository.AddAsync(outboxMessage,cancellationToken);


                // 7. Mark consumed event
                // This is procceed Events save to DB
                await _idempotencyService.MarkAsProcessedAsync(notification.EventId, nameof(PriceDataCollectedEvent), cancellationToken);

                // 8. Commit EVERYTHING

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
   
                Console.WriteLine($"Error processing PriceDataCollectedEvent: {ex.Message}");
                throw; 
                // Rolleback
            }
            

        }
    }
}
