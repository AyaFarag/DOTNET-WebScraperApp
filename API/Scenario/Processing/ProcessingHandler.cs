using API.Scenario.Shared;
using MediatR;

namespace API.Scenario.Processing
{
    public class ProcessingHandler : INotificationHandler<PriceScrapedEvent>
    {
        private readonly IMediator _mediator;

        public ProcessingHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(PriceScrapedEvent notification, CancellationToken ct)
        {
            // Normalize "10 AED"
            var parts = notification.RawPrice.Split(' ');
            decimal price = decimal.Parse(parts[0]);
            string currency = parts[1];

            await _mediator.Publish(new PriceProcessedEvent
            {
                ProductName = notification.ProductName.ToLower(),
                Price = price,
                Currency = currency
            });
        }
    }
}
