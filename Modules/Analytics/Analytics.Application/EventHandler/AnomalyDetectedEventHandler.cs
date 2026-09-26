using Analytics.Application.Interfaces.AI.Service;
using MediatR;
using Shared.Contracts.Events.Analytics;

namespace Analytics.Application.EventHandler
{
    public sealed class AnomalyDetectedEventHandler : INotificationHandler<AnomalyDetectedEvent>
    {
        private readonly IAnomalyExplanationService _service;

        public AnomalyDetectedEventHandler(IAnomalyExplanationService service)
        {
            _service = service;
        }

        public async Task Handle(AnomalyDetectedEvent notification,CancellationToken cancellationToken)
        {
            await _service.ExplainAsync(notification.AnomalyId, cancellationToken);
        }
    }
}
