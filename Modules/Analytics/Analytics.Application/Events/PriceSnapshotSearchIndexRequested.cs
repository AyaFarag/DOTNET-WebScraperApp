using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Events
{
    public sealed record PriceSnapshotSearchIndexRequested(
    Guid EventId,
    Guid SnapshotId);
}
