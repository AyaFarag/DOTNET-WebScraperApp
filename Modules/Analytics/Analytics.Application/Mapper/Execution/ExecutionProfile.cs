using AutoMapper;

namespace Analytics.Application.Mapper.Execution
{
    public class ExecutionProfile : Profile
    {
        public ExecutionProfile()
        {
            CreateMap<Domain.Entities.AnalyticsExecution, DTOs.AnalyticsExecutionDto>().ReverseMap();
        }
    }
}
