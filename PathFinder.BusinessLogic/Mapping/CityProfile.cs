using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.City;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;
namespace PathFinder.BusinessLogic.Mapping
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<CityDTO, City>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, CityDTO>().ReverseMap();
            CreateMap<City, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<City>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}
