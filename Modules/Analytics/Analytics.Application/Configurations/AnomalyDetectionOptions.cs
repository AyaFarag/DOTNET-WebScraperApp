using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Configurations
{
    public sealed class AnomalyDetectionOptions
    {
        public int MinimumObservations { get; set; } = 10;

        public decimal IqrMultiplier { get; set; } = 1.5m;
    }
}
