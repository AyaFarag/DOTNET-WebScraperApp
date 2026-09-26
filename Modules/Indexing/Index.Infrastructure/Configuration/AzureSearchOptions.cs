using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Infrastructure.Configuration
{
    public sealed class AzureSearchOptions
    {
        public const string SectionName = "AzureSearch";

        public string Endpoint { get; set; } = null!;

        public string ApiKey { get; set; } = null!;

        public string IndexName { get; set; } = "products";
    }
}
