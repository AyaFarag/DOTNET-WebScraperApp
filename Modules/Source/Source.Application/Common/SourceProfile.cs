using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Application.Common
{
    public class SourceProfile : Profile
    {
        public SourceProfile()
        {
           CreateMap<Source.Domain.Entities.Source, Source.Application.DTOs.SourceDTO>().ReverseMap();
           CreateMap<Source.Domain.Entities.Source, Source.Application.DTOs.SourceRequest>().ReverseMap();
           CreateMap<Source.Domain.Entities.Source, Source.Application.DTOs.UpdateSourceRequest>().ReverseMap()
                .ForAllMembers(options =>
                     options.Condition((src, dest, srcMember) =>
                  srcMember != null));

        }
    }
}
