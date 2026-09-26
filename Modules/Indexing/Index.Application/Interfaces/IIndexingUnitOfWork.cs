namespace Index.Application.Interfaces
{
    public interface IIndexingUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
