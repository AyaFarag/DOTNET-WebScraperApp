using Index.Application.CQRS.Command;
using Index.Application.Interfaces;
using Index.Application.Interfaces.Repository;
using Index.Application.Interfaces.Search;
using Index.Application.Mapper;
using Index.Domain.Entities;
using MediatR;
using Shared.Contracts.Events.Index;
using System.Text.Json;

namespace Index.Infrastructure.Presistance.Repositories.BackgroundJobs
{
    public sealed class IndexingOutboxPublisherJob
    {
        private readonly IIndexingOutboxMessage _outboxRepository;
        private readonly IIndexingRepository _indexingRepository;
        private readonly ISearchIndexWriter _searchIndex;
        private readonly IPublisher _publisher;
        private readonly IIndexingUnitOfWork _unitOfWork;

        public IndexingOutboxPublisherJob(
            IIndexingOutboxMessage outboxRepository,
            IIndexingRepository indexingRepository,
            ISearchIndexWriter searchIndex,
            IPublisher publisher,
            IIndexingUnitOfWork unitOfWork)
        {
            _outboxRepository = outboxRepository;
            _indexingRepository = indexingRepository;
            _searchIndex = searchIndex;
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
                    message.MarkAsFailed(ex.Message);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishAsync(OutboxMessage message,CancellationToken cancellationToken)
        {
            switch (message.EventType)
            {
                case nameof(IndexProductCommand):

                    await HandleIndexProductCommand(
                        message,
                        cancellationToken);

                    break;

                case nameof(PriceDataIndexedEvent):

                    await HandlePriceDataIndexedEvent(
                        message,
                        cancellationToken);

                    break;

                default:

                    throw new InvalidOperationException($"Unknown outbox message type: {message.EventType}");
            }
        }

        private async Task HandleIndexProductCommand(OutboxMessage message,CancellationToken cancellationToken)
        {
            var command = JsonSerializer.Deserialize<IndexProductCommand>(message.Payload);

            if (command is null)
            {
                throw new InvalidOperationException(
                    "Invalid IndexProductCommand payload.");
            }

            var product = await _indexingRepository.GetByIdAsync(command.IndexedProductId,cancellationToken);

            if (product is null)
            {
                throw new InvalidOperationException(
                    $"Indexed product {command.IndexedProductId} not found.");
            }

            var document = IndexedProductSearchMapper.Map(product);

            await _searchIndex.IndexAsync(document,cancellationToken);
        }

        private async Task HandlePriceDataIndexedEvent(OutboxMessage message,CancellationToken cancellationToken)
        {
            var @event = JsonSerializer.Deserialize<PriceDataIndexedEvent>(message.Payload);

            if (@event is null)
            {
                throw new InvalidOperationException(
                    "Invalid PriceDataIndexedEvent payload.");
            }

            await _publisher.Publish(@event, cancellationToken);
        }
    }
}
