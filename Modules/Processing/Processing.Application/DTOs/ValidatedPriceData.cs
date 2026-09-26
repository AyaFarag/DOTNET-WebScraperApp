using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Application.DTOs
{
    public sealed record ValidatedPriceData
    {
        public Guid RawPriceId { get; set; }
        public Guid BatchId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}
