using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.DTOs
{
    public class RawPriceDto
    {
        public string ProductName { get; set; } = "";
        public string RawPrice { get; set; } = "";
        public string Source { get; set; } = "";
        public DateTime CollectedAt { get; set; }
        public string SourceUrl { get; set; } = null!;

        public string? Currency { get; set; }

        public string? RawData { get; set; }
    }
}
