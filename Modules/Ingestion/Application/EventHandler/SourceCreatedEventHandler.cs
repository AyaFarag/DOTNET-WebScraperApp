using MediatR;
using Shared.Contracts.Events.Source;

namespace Application.EventHandler
{
    public class SourceCreatedEventHandler : INotificationHandler<SourceCreatedEvent>
    {
        public Task Handle(SourceCreatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
