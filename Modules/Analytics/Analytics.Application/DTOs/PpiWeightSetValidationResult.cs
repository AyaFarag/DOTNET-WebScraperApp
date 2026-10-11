using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.DTOs
{
    public sealed record PpiWeightSetValidationResult
    {
        public DateTime Date { get; init; }

        public int WeightCount { get; init; }

        public decimal TotalWeight { get; init; }

        public decimal TotalWeightPercentage { get; init; }

        public bool IsValid { get; init; }

        public string? ErrorMessage { get; init; }
    }
}
