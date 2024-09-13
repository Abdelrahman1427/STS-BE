using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.CompanySector;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class CompanySectorProfile : Profile
    {
        public CompanySectorProfile()
        {
            CreateMap<CompanySector, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<CompanySectorDTO, CompanySector>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, CompanySectorDTO>().ReverseMap();
            CreateMap<CompanySector, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<CompanySector>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}