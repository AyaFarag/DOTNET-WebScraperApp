using MediatR;

namespace API.Scenario.Shared
{
    public class PriceScrapedEvent : INotification
    {
        public string ProductName { get; set; } = "";
        public string RawPrice { get; set; } = "";
        public string Source { get; set; } = "";    
    }
}
