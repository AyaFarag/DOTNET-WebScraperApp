namespace Index.Application.Interfaces.Search
{
    public interface IAzureSearchIndexInitializer
    {
        Task InitializeAsync(CancellationToken cancellationToken);
    }
}
