using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Application.DTOs
{
    public sealed class ProductClassificationResult
    {
        public string CategoryCode { get; init; } = null!;

        public string SubCategoryCode { get; init; } = null!;

        public string ProductGroupCode { get; init; } = null!;

        public string ProductTypeCode { get; init; } = null!;

        public double Confidence { get; init; }

        public string Reason { get; init; } = null!;
    }
}
