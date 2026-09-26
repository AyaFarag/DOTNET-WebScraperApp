namespace Source.Application.DTOs
{
    public class UpdateSourceRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ProductSelector { get; set; } = string.Empty;
        public string PriceSelector { get; set; } = string.Empty;
    }
}
