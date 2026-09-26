using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Enums
{
    public enum DateRangeType
    {
        None,
        Today,
        Yesterday,
        ThisWeek,
        ThisMonth,
        Last7Days,
        Last30Days,
        Custom
    }
}
