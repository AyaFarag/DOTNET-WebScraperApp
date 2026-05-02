using API.Scenario.Shared;
using MediatR;

namespace API.Scenario.Validation
{
    public class PriceValidationHandler : INotificationHandler<PriceScrapedEvent>
    {
        public Task Handle(PriceScrapedEvent notification, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(notification.RawPrice))
                throw new Exception("Invalid price");

            return Task.CompletedTask;
        }
    }
}
