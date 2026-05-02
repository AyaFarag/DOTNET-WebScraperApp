using MediatR;

namespace API.Scenario.Processing
{
    public class PriceProcessedEvent : INotification
    {
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public string Currency { get; set; } = "";
    }
}
