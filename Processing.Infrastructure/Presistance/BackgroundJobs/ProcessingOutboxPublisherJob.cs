using MediatR;
using Processing.Application.Interfaces.Repositories;
using Processing.Domain.Entities;
using Shared.Contracts.Events.Processing;
using System.Text.Json;

namespace Processing.Infrastructure.Presistance.BackgroundJobs
{
    public sealed class ProcessingOutboxPublisherJob
    {
        private readonly IOutboxRepository _outboxRepository;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;

        public ProcessingOutboxPublisherJob(
            IOutboxRepository outboxRepository,
            IPublisher publisher,
            IUnitOfWork unitOfWork)
        {
            _outboxRepository = outboxRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            var messages =
                await _outboxRepository.GetPendingAsync(
                    50,
                    cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    await PublishAsync(
                        message,
                        cancellationToken);

                    message.MarkAsProcessed();
                }
                catch (Exception ex)
                {
                    message.MarkAsFailed(
                        ex.Message);
                }
            }

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }

        private async Task PublishAsync(
            OutboxMessage message,
            CancellationToken cancellationToken)
        {
            if (message.EventType ==
                nameof(PriceDataProcessedEvent))
            {
                var @event =
                    JsonSerializer.Deserialize<
                        PriceDataProcessedEvent>(
                        message.Payload);

                if (@event is null)
                {
                    throw new InvalidOperationException(
                        "Invalid PriceDataProcessedEvent.");
                }

                await _publisher.Publish(
                    @event,
                    cancellationToken);
            }
        }
    }
}
