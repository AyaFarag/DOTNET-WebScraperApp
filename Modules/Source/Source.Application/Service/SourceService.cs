using AutoMapper;
using MediatR;
using Shared.Contracts.Events;
using Shared.Contracts.Events.Source;
using Source.Application.DTOs;
using Source.Application.Interfaces.Repository;
using Source.Application.Interfaces.Service;

namespace Source.Application.Service
{
    public class SourceService : ISourceService
    {
        private readonly ISourceRepository _sourceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public IMapper mapper;
        public IPublisher publisher;
        public SourceService(ISourceRepository sourceRepository, IUnitOfWork unitOfWork,IMapper mapper, IPublisher publisher)
        {
            _sourceRepository = sourceRepository;
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public async Task<IEnumerable<SourceDTO>> GetAllSourcesAsync()
        {
            var sources = await _sourceRepository.GetAllAsync();
            return sources.Select(s => mapper.Map<SourceDTO>(s));
        }

        public async Task<SourceDTO> GetSourceByIdAsync(Guid id)
        {
            var source = await _sourceRepository.GetByIdAsync(id);
            return mapper.Map<SourceDTO>(source);
        }
        public async Task AddSourceAsync(SourceRequest sourceRequest, CancellationToken cancellationToken)
        {
            var newsource = mapper.Map<Domain.Entities.Source>(sourceRequest);
            await _sourceRepository.AddAsync(newsource);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            await publisher.Publish(new SourceCreatedEvent()
            {
                SourceId = newsource.Id
            },
            cancellationToken);

        }
        public async Task UpdateSourceAsync(Guid id,UpdateSourceRequest updatesourcerequest, CancellationToken cancellationToken)
        {
            var source = await _sourceRepository.GetByIdAsync(id);
            if (source == null)
                throw new ArgumentException("Source not found");

            mapper.Map(updatesourcerequest , source);
          //  await _sourceRepository.UpdateAsync(id,source);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteSourceAsync(Guid id, CancellationToken cancellationToken)
        {
            var source = await _sourceRepository.GetByIdAsync(id);
            if (source == null)
                throw new ArgumentException("Source not found");

            await _sourceRepository.DeleteAsync(source);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DisableSource(Guid id, CancellationToken cancellationToken)
        {
            await _sourceRepository.DisableSource(id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> EnableSource(Guid id, CancellationToken cancellationToken)
        {
            await _sourceRepository.EnableSource(id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

    
    }
}
