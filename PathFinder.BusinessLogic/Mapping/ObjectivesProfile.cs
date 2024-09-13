using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.Objectives;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class ObjectivesProfile : Profile
    {
        public ObjectivesProfile()
        {
            CreateMap<Objective, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<ObjectivesDTO, Objective>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, ObjectivesDTO>().ReverseMap();
            CreateMap<Objective, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<Objective>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}