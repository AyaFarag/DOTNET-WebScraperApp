using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PriceScrapedEvent : INotification
    {
        public string ProductName { get; set; } = "";
        public string RawPrice { get; set; } = "";
        public string Source { get; set; } = "";
    }
}
