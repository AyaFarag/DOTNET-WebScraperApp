using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.DTOs
{
    public sealed record MarketAnalysisDto
    {
        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string Currency { get; init; } = string.Empty;

        public decimal MarketAveragePrice { get; init; }

        public decimal MarketMinimumPrice { get; init; }

        public decimal MarketMaximumPrice { get; init; }

        public decimal PriceSpread { get; init; }

        public decimal? PriceSpreadPercentage { get; init; }

        public SupplierPriceDto? CheapestSupplier { get; init; }

        public SupplierPriceDto? MostExpensiveSupplier { get; init; }

        public IReadOnlyCollection<SupplierPriceDto> Suppliers { get; init; }
            = [];
    }
}
