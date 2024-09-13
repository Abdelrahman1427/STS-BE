using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.EngagementLevel;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class EngagementLevelProfile : Profile
    {
        public EngagementLevelProfile()
        {
            CreateMap<EngagementLevel, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<EngagementLevelDTO, EngagementLevel>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, EngagementLevelDTO>().ReverseMap();
            CreateMap<EngagementLevel, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<EngagementLevel>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}