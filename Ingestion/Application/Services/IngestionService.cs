using Ingestion.Application.CQRS.Query;
using Ingestion.Application.Events;
using Ingestion.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.Services
{
    public class IngestionService
    {
        
        private IMediator _mediator ;

        public IngestionService( IMediator mediator)
        {
            
            _mediator = mediator;
        }

        public async Task RunScrapingAsync(ScrapePricesQuery query)
        {
            var data = await _mediator.Send(query);

            foreach (var item in data)
            {
                await _mediator.Publish(new PriceScrapedEvent
                {
                    ProductName = item.ProductName,
                    RawPrice = item.RawPrice,
                    Source = item.Source
                });
            }
        }
    }
}
