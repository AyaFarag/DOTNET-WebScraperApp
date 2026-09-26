using Analytics.Application.EventHandler;
using Analytics.Application.Events;
using Analytics.Application.Interfaces.Repositories;
using Hangfire;
using MediatR;

namespace Analytics.Infrastructure.Persistence.Service.BackgroundJobs
{
    public class OutboxPublisherJob
    {
        private readonly IPublisher _publisher;
        private readonly IAnalyticsUnitOfWork _unitOfWork;
        private readonly IOutboxRepository _outboxRepository;
        private readonly PriceSnapshotSearchIndexHandler _searchHandler;

        public OutboxPublisherJob(
            IPublisher publisher,
            IAnalyticsUnitOfWork unitOfWork,
            IOutboxRepository outboxRepository,
            PriceSnapshotSearchIndexHandler searchHandler)
        {
            _outboxRepository = outboxRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
            _searchHandler = searchHandler;
        }

        //public async Task ExecuteAsync(CancellationToken cancellationToken)
        //{
        //    var messages = await _outboxRepository.GetPendingAsync(50, cancellationToken);

        //    foreach (var message in messages)
        //    {
        //        try
        //        {
        //            await PublishAsync(message, cancellationToken);

        //            await _outboxRepository.MarkAsProcessedAsync(message, cancellationToken);
        //        }
        //        catch (Exception ex)
        //        {
        //            await _outboxRepository.MarkAsFailedAsync(message, ex.Message, cancellationToken);
        //        }
        //    }

        //    await _unitOfWork.SaveChangesAsync(cancellationToken);
        //}



        //private async Task PublishAsync(OutboxMessage message, CancellationToken cancellationToken)
        //{
        //    if (message.Type.Contains(nameof(AnalyticsCompletedEvent)))
        //    {
        //        var @event = JsonSerializer.Deserialize<AnalyticsCompletedEvent>(message.Payload);

        //        if (@event is null)
        //            throw new InvalidOperationException(
        //                "Unable to deserialize AnalyticsCompletedEvent.");

        //        await _publisher.Publish(@event, cancellationToken);
        //    }
        //}

        [AutomaticRetry(Attempts = 0)]
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            const int batchSize = 50;

            var messages = await _outboxRepository.GetPendingAsync(batchSize, cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    await DispatchAsync(
                        message.Type,
                        message.Payload,
                        cancellationToken);

                    await _outboxRepository.MarkAsProcessedAsync( message, cancellationToken);
                }
                catch (Exception exception)
                {
                    await _outboxRepository.MarkAsFailedAsync( message, exception.ToString(),cancellationToken);
                }
            }
        }
        private async Task DispatchAsync( string type, string payload, CancellationToken cancellationToken)
        {
            if (type ==
                typeof(
                    PriceSnapshotSearchIndexRequested)
                    .FullName)
            {
                await _searchHandler.HandleAsync(
                    payload,
                    cancellationToken);

                return;
            }

            throw new InvalidOperationException(
                $"Unknown outbox message type '{type}'.");
        }
    }
}
