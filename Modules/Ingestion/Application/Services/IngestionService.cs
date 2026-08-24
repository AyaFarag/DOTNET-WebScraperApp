using Ingestion.Application.CQRS.Query;
using Ingestion.Application.DTOs;
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
    public class IngestionService : IIngestionService
    {
        
        private IMediator _mediator ;
        private readonly IScraper _scraper;

        public IngestionService(IScraper scraper, IMediator mediator)
        {
            _scraper = scraper;
            _mediator = mediator;
        }

        public async Task<List<RawPriceDto>> RunScrapingAsync()
        {
        
            var data = await _scraper.ScrapeAsync();

            foreach (var item in data)
            {
                await _mediator.Publish(new PriceScrapedEvent
                {
                //    ProductName = item.ProductName,
                //    RawPrice = item.RawPrice,
                //    Source = item.Source
                });
            }

            return data;
        }
       
       
        public async Task<HashSet<string>> RunEmailScrapingAsync()
        {
     
            var data = await _scraper.EmailScrapeAsync();

            foreach (var item in data)
            {
                await _mediator.Publish(new EmailScrapedEvent
                {
                    // Email = item.email
               
                });
            }

            return data;
        }
    }
}
