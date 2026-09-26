namespace Shared.Contracts.Queries.Source
{
    public interface ISourceQueryReader<TSource> where TSource : class
    {
        Task<IEnumerable<TSource>> GetAllSourceQueryAsync();
    }
}
