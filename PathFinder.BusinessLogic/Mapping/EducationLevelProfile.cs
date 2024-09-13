using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.EducationLevel;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class EducationLevelProfile : Profile
    {
        public EducationLevelProfile()
        {
            CreateMap<EducationLevel, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<EducationLevelDTO, EducationLevel>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, EducationLevelDTO>().ReverseMap();
            CreateMap<EducationLevel, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<EducationLevel>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}