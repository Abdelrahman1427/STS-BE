using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.Assessment;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class AssessmentProfile : Profile
    {
        public AssessmentProfile()
        {
            CreateMap<Assessment, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<AssessmentDTO, Assessment>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedByUser, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, AssessmentDTO>().ReverseMap();
            CreateMap<Assessment, GetLookUpDefinitionDTO>()
                .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
                .ReverseMap();
            CreateMap<Pagination<Assessment>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}