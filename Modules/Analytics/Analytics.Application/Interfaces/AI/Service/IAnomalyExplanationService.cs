using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Interfaces.AI.Service
{
    public interface IAnomalyExplanationService
    {
        Task ExplainAsync(
            Guid anomalyId,
            CancellationToken cancellationToken);
    }
}
