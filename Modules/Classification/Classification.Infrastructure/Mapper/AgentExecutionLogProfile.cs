using AutoMapper;
using Classification.Domain.Enttities;
using Shared.Contracts.Agents;

namespace Classification.Infrastructure.Mapper
{
    public class AgentExecutionLogProfile : Profile
    {
        public AgentExecutionLogProfile()
        {
            CreateMap<Domain.Enttities.AgentExecutionLog, AgentExecutionLogEntry>().ReverseMap();
        }
    }
}
