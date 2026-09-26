using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Infrastructure.Configurations
{
    public sealed class AzureSearchOptions
    {
        public const string SectionName = "AzureSearch";

        public string Endpoint { get; init; } = string.Empty;

        public string ApiKey { get; init; } = string.Empty;

        public string PriceSnapshotIndexName { get; init; } =
            "analytics-price-snapshots";
    }
}
