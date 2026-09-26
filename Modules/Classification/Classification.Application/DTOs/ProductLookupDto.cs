namespace Classification.Application.DTOs
{
    public sealed record ProductLookupDto
    {
        public string ProductKey { get; init; } = string.Empty;

        public string? ProductName { get; init; }

        public string? Brand { get; init; }

        public string? CategoryCode { get; init; }
    }
}
