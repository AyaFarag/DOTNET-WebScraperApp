using Source.Application.DTOs;

namespace Source.Application.Interfaces.Service
{
    public interface ISourceService
    {
        Task<IEnumerable<SourceDTO>> GetAllSourcesAsync();
        Task<SourceDTO> GetSourceByIdAsync(Guid id);
        Task AddSourceAsync(SourceRequest sourceRequest, CancellationToken cancellationToken);
        Task UpdateSourceAsync(Guid id ,UpdateSourceRequest updatesourcerequest, CancellationToken cancellationToken);
        Task DeleteSourceAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> DisableSource(Guid id, CancellationToken cancellationToken);
        Task<bool> EnableSource(Guid id, CancellationToken cancellationToken);
    }
}
