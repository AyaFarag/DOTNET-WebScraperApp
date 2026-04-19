using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configurations
{
    public class ScraperOptions
    {
        public bool Headless { get; set; }
        public int Timeout { get; set; }
        public List<ScraperSource> Sources { get; set; } = new();
    }

    public class ScraperSource
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Url { get; set; }
        public string ProductSelector { get; set; }
        public string PriceSelector { get; set; }
    }
}
