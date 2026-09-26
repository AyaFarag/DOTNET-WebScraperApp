using Classification.Application.Interfaces;
using Classification.Domain.Enttities;
using MediatR;
using Shared.Contracts.Events.Classification;
using Shared.Contracts.Events.Processing;
using System.Text.Json;

namespace Classification.Infrastructure.Repositories.BackgroundJobs
{
    public sealed class ClassificationOutboxPublisherJob
    {
        private readonly IOutboxRepository _outboxRepository;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;

        public ClassificationOutboxPublisherJob(
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
            var messages = await _outboxRepository.GetPendingAsync(50,cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    await PublishAsync(message,cancellationToken);

                    message.MarkAsProcessed();
                }
                catch (Exception ex)
                {
                    message.MarkAsFailed(ex.Message, DateTime.UtcNow.AddMicroseconds(2));
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishAsync(OutboxMessage message,CancellationToken cancellationToken)
        {
            if (message.Type == nameof(PriceDataClassifiedEvent))
            {
                var @event = JsonSerializer.Deserialize<PriceDataClassifiedEvent>(message.Payload);

                if (@event is null)
                {
                    throw new InvalidOperationException(
                        "Invalid PriceDataClassifiedEvent.");
                }

                await _publisher.Publish(@event,cancellationToken);
            }
        }
    }
}
