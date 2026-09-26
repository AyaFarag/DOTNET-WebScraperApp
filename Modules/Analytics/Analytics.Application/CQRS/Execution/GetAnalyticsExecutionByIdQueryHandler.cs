using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Analytics.Application.CQRS.Execution
{
    public class GetAnalyticsExecutionByIdQueryHandler : IRequestHandler<GetAnalyticsExecutionByIdQuery, AnalyticsExecutionDto?>
    {
        private readonly IMapper _mapper;
        private readonly IAnalyticsExecutionRepository _analyticsExecutionRepository;
        public GetAnalyticsExecutionByIdQueryHandler(IAnalyticsExecutionRepository analyticsExecutionRepository, IMapper mapper)
        {
            _analyticsExecutionRepository = analyticsExecutionRepository;
            _mapper = mapper;
        }
        public async Task<AnalyticsExecutionDto?> Handle(GetAnalyticsExecutionByIdQuery request, CancellationToken cancellationToken)
        {
            var execution = await _analyticsExecutionRepository.GetByIdAsync(request.Id, cancellationToken);
            var mapExecution = _mapper.Map<AnalyticsExecutionDto>(execution);
            return mapExecution;
        }
    }
}
