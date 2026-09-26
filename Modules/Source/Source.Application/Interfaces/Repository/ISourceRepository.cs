namespace Source.Application.Interfaces.Repository
{
    public interface ISourceRepository : IRepository<Source.Domain.Entities.Source>
    {
        // Define methods for CRUD operations on Source entities
        //Task<IEnumerable<Source.Domain.Entities.Source>> GetAllSourcesAsync();
        //Task<Source.Domain.Entities.Source> GetSourceByIdAsync(Guid id);
        //Task AddSourceAsync(Source.Domain.Entities.Source source);
        //Task UpdateSourceAsync(Source.Domain.Entities.Source source);
        //Task DeleteSourceAsync(Guid id);
        Task<bool> DisableSource(Guid id);
        Task<bool> EnableSource(Guid id);
    }
}
