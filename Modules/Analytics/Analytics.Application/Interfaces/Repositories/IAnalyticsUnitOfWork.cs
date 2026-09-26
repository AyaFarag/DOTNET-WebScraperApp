namespace Analytics.Application.Interfaces.Repositories
{
    public interface IAnalyticsUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
