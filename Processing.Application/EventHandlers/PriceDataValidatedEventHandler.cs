using AutoMapper;
using MediatR;
using Processing.Application.DTOs;
using Processing.Application.Interfaces.Repositories;
using Processing.Application.Interfaces.Services;
using Processing.Domain.Entities;
using Shared.Contracts.Events.Processing;
using Shared.Contracts.Events.Validation;
using Shared.Contracts.Queries.Validation;
using Shared.Domain;
using System.Text.Json;

namespace Processing.Application.EventHandlers
{
    public sealed class PriceDataValidatedEventHandler : INotificationHandler<PriceDataValidatedEvent>
    {
        public readonly IMapper _mapper;
        private readonly IIdempotencyService _idempotencyService;
        private readonly IValidatedPriceQuery _validatedPriceReader;
        private readonly IProcessingService _processingService;
        private readonly IProcessedPriceRepository _processedPriceRepository;
        private readonly IOutboxRepository _outboxRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PriceDataValidatedEventHandler(
            IMapper mapper,
            IIdempotencyService idempotencyService,
            IValidatedPriceQuery validatedPriceReader,
            IProcessingService processingService,
            IProcessedPriceRepository processedPriceRepository,
            IOutboxRepository outboxRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _idempotencyService = idempotencyService;
            _validatedPriceReader = validatedPriceReader;
            _processingService = processingService;
            _processedPriceRepository =
                processedPriceRepository;
            _outboxRepository = outboxRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            PriceDataValidatedEvent notification,
            CancellationToken cancellationToken)
        {
            // 1. Idempotency

            var alreadyProcessed =
                await _idempotencyService.IsProcessedAsync( notification.EventId, cancellationToken);

            if (alreadyProcessed)
                return;


            // 2. Load validated data

            var validatedPrices =
                await _validatedPriceReader.GetValidPricesAsync(notification.BatchId,cancellationToken);

            // mapper
            var validatedPriceDtos = _mapper.Map<List<RawPrice>>(validatedPrices);
            // 3. Process

            var result =
                await _processingService.ProcessAsync(notification.BatchId, validatedPriceDtos, cancellationToken);


            // 4. Save ProcessedPrice


            await _processedPriceRepository.AddRangeAsync(result.ProcessedPrices,cancellationToken);


            // 5. Create next event

            var processedEvent =
                new PriceDataProcessedEvent(
                    Guid.NewGuid(),
                    notification.BatchId,
                    result.TotalCount,
                    result.ProcessedCount,
                    result.FailedCount);


            // 6. Add Outbox

            var outboxMessage = new OutboxMessage(processedEvent.EventId,
                    nameof(PriceDataProcessedEvent), JsonSerializer.Serialize(processedEvent));

            await _outboxRepository.AddAsync(outboxMessage,cancellationToken);


            // 7. Mark incoming event processed

            await _idempotencyService.MarkAsProcessedAsync(notification.EventId,
                    nameof(PriceDataValidatedEvent),
                    cancellationToken);


            // 8. Commit everything

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
