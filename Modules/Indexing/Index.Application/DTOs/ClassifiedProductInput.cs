using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.DTOs
{
    public sealed class ClassifiedProductInput
    {
        public Guid ClassificationResultId { get; init; }

        public Guid ProcessedPriceId { get; init; }

        public string ProductKey { get; init; } = null!;

        public string ProductName { get; init; } = null!;

        public string? Brand { get; init; }

        public string CategoryCode { get; init; } = null!;

        public string SubCategoryCode { get; init; } = null!;

        public string ProductGroupCode { get; init; } = null!;

        public string ProductTypeCode { get; init; } = null!;

        public decimal Price { get; init; }

        public string Currency { get; init; } = null!;

        public decimal? UnitPrice { get; init; }

        public string? Unit { get; init; }

        public string Source { get; init; } = null!;

        public DateTime CollectedAt { get; init; }
    }
}
