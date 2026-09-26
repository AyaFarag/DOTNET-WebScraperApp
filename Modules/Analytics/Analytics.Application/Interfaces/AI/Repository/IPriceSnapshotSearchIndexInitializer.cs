using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Interfaces.AI.Repository
{
    public interface IPriceSnapshotSearchIndexInitializer
    {
        Task InitializeAsync(
            CancellationToken cancellationToken);
    }
}
