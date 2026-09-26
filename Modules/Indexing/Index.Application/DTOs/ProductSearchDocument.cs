using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.DTOs
{
    public sealed class ProductSearchDocument
    {
        public string Id { get; set; } = null!;

        public string ProductKey { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string? Brand { get; set; }

        public string CategoryCode { get; set; } = null!;

        public string SubCategoryCode { get; set; } = null!;

        public string ProductGroupCode { get; set; } = null!;

        public string ProductTypeCode { get; set; } = null!;

        public double Price { get; set; }

        public string Currency { get; set; } = null!;

        public double? UnitPrice { get; set; }

        public string? Unit { get; set; }

        public string Source { get; set; } = null!;

        public DateTime CollectedAt { get; set; }
    }
}
