using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.DTOs
{
    public sealed class IndexingResultDto
    {
        public Guid BatchId { get; init; }

        public Guid ExecutionId { get; init; }

        public int TotalProducts { get; init; }

        public int IndexedProducts { get; init; }

        public int FailedProducts { get; init; }

        public string Status { get; init; } = null!;
    }
}
