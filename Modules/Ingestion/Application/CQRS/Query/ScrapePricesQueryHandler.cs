using Ingestion.Application.DTOs;
using MediatR;
using Ingestion.Application.Interfaces;

namespace Ingestion.Application.CQRS.Query
{
    public class ScrapePricesQueryHandler : IRequestHandler<ScrapePricesQuery, List<RawPriceDto>>
    {
        private readonly IScraper _service;

        public ScrapePricesQueryHandler(IScraper service)
        {
            _service = service;
            
        }

        public async Task<List<RawPriceDto>> Handle(ScrapePricesQuery request, CancellationToken cancellationToken)
        {
            var data = await _service.ScrapeAsync();
               
               return data;
        }
    }
}
