using Ingestion.Application.DTOs;
using Ingestion.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.CQRS.Query
{
    public class ScrapePricesQueryHandler : IRequestHandler<ScrapePricesQuery, HashSet<string>>
    {
        private readonly IScraper _playwrightScraper;
        public ScrapePricesQueryHandler(IScraper scraper)
        {
            _playwrightScraper = scraper;
        }

        public async Task<HashSet<string>> Handle(ScrapePricesQuery request, CancellationToken cancellationToken)
        {
            var data = await _playwrightScraper.ScrapeAsync();
            return data;
        }
    }
}
