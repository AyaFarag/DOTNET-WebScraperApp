using API.Scenario.Processing;
using MediatR;

namespace API.Scenario.Classification
{
    public class ClassificationHandler : INotificationHandler<PriceProcessedEvent>
    {
        private readonly IMediator _mediator;

        public ClassificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(PriceProcessedEvent notification, CancellationToken ct)
        {
            string category = notification.ProductName.Contains("rice")
                ? "Food"
                : "Other";

            await _mediator.Publish(new PriceClassifiedEvent
            {
                ProductName = notification.ProductName,
                Price = notification.Price,
                Category = category
            });
        }
    }
}
