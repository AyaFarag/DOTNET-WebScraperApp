using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.Configurations
{
    public class ScraperOptions
    {
        public bool Headless { get; set; }
        public int Timeout { get; set; }
        public List<ScraperSource> Sources { get; set; } = new();
    }

    public class ScraperSource
    {
        public string Name { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ProductSelector { get; set; } = string.Empty;
        public string PriceSelector { get; set; } = string.Empty;
    }
}
