namespace API.Scenario.Persistence.Entities
{
    public class PriceRecord
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
    }
}
