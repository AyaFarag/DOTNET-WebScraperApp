using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.DTOs
{
    public sealed record SupplierPriceDto
    {
        public string Source { get; init; } = string.Empty;

        public decimal Price { get; init; }

        public decimal DifferenceFromMarketAverage { get; init; }

        public decimal? DifferencePercentage { get; init; }

        public int Rank { get; init; }
    }
}
