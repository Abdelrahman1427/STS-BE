using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.EngagementModality;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class EngagementModalityProfile : Profile
    {
        public EngagementModalityProfile()
        {
            CreateMap<EngagementModality, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<EngagementModalityDTO, EngagementModality>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, EngagementModalityDTO>().ReverseMap();
            CreateMap<EngagementModality, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<EngagementModality>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}