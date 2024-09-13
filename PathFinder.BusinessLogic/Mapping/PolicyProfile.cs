using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.Policy;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class PolicyProfile : Profile
    {
        public PolicyProfile()
        {
            CreateMap<Policy, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<PolicyDTO, Policy>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, PolicyDTO>().ReverseMap();
            CreateMap<Policy, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<Policy>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}