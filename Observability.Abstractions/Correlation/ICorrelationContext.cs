using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Observability.Abstractions.Correlation;

public interface ICorrelationContext
{
    string Id { get; }
    void Set(string id);
}
