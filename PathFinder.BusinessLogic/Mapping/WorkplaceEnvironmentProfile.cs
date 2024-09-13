using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.WorkplaceEnvironment;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class WorkplaceEnvironmentProfile : Profile
    {
        public WorkplaceEnvironmentProfile()
        {
            CreateMap<WorkplaceEnvironment, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<WorkplaceEnvironmentDTO, WorkplaceEnvironment>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, WorkplaceEnvironmentDTO>().ReverseMap();
            CreateMap<WorkplaceEnvironment, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<WorkplaceEnvironment>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}