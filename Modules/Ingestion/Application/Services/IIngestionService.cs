using Ingestion.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.Services
{
    public interface IIngestionService
    {
        Task<List<RawPriceDto>> RunScrapingAsync();

        Task<HashSet<string>> RunEmailScrapingAsync();
    }
}