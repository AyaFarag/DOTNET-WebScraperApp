using Application.Interfaces.Repositories;
using Ingestion.Domain.Entities;
using MediatR;
using Shared.Contracts.Events.Ingestion;
using System.Text.Json;

namespace Infrastructure.Services.BackgroundJobs
{
    public sealed class OutboxPublisherJob
    {
        private readonly IOutboxRepository _outboxRepository;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;

        public OutboxPublisherJob(
            IOutboxRepository outboxRepository,
            IPublisher publisher,
            IUnitOfWork unitOfWork)
        {
            _outboxRepository = outboxRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var messages = await _outboxRepository.GetPendingAsync(50, cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    await PublishAsync(message, cancellationToken);

                    await _outboxRepository.MarkAsProcessedAsync(message, cancellationToken);
                }
                catch (Exception ex)
                {
                    await _outboxRepository.MarkAsFailedAsync(message, ex.Message, cancellationToken);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }



        private async Task PublishAsync(OutboxMessage message,CancellationToken cancellationToken)
        {
            if (message.Type.Contains(nameof(PriceDataCollectedEvent)))
            {
                var @event = JsonSerializer.Deserialize<PriceDataCollectedEvent>(message.Payload);

                if (@event is null)
                    throw new InvalidOperationException(
                        "Unable to deserialize PriceDataCollectedEvent.");

                await _publisher.Publish(@event,cancellationToken);
            }
        }
    }
}
