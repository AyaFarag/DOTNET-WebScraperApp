using MediatR;
using Shared.Contracts.Events.Ingestion;

namespace Ingestion.Application.Events.Handler
{
    public class EmailScrapedHandler : INotificationHandler<EmailScrapedEvent>
    {
        public Task Handle(EmailScrapedEvent notification, CancellationToken ct)
        {
            Console.WriteLine($"[EVENT RECEIVED] ");
          //  Console.WriteLine($"[EVENT RECEIVED] {notification.ProductName} - {notification.RawPrice}");
            return Task.CompletedTask;
        }
    }
}
