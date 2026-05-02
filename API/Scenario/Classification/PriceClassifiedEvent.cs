using MediatR;

namespace API.Scenario.Classification
{
    public class PriceClassifiedEvent : INotification
    {
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
    }
}
