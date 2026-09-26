using AutoMapper;
using Processing.Application.DTOs;
using Shared.Domain;

namespace Processing.Application.Common.Automapper
{
    public class RawPriceProfile : Profile
    {
        public RawPriceProfile()
        {
            CreateMap<RawPrice, RawPriceData>().ReverseMap();
        }
    }
}
