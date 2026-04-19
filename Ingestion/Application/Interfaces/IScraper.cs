using Ingestion.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.Interfaces
{
    public interface IScraper
    {
        Task<List<RawPriceDto>> ScrapeAsync();
    }
}
