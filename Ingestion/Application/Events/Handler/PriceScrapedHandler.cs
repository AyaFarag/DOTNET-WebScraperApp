using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.Events.Handler
{
    public class PriceScrapedHandler : INotificationHandler<PriceScrapedEvent>
    {
        public Task Handle(PriceScrapedEvent notification, CancellationToken ct)
        {
            Console.WriteLine($"[EVENT RECEIVED] ");
          //  Console.WriteLine($"[EVENT RECEIVED] {notification.ProductName} - {notification.RawPrice}");
            return Task.CompletedTask;
        }
    }
}
