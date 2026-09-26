using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Analytics.Application.CQRS.Execution
{
    public class GetAnalyticsExecutionsQueryHandler : IRequestHandler<GetAnalyticsExecutionsQuery, PagedResult<AnalyticsExecutionDto>>
    {
        private readonly IAnalyticsExecutionRepository _repository;
        private readonly IMapper _mapper;
        public GetAnalyticsExecutionsQueryHandler(IAnalyticsExecutionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<AnalyticsExecutionDto>> Handle(GetAnalyticsExecutionsQuery request, CancellationToken cancellationToken)
        {
            var executions = await _repository.GetAsync(request.Page, request.PageSize, request.Status, cancellationToken);
            return  _mapper.Map<PagedResult<AnalyticsExecution>, PagedResult<AnalyticsExecutionDto>>(executions);
        }
    }
}
