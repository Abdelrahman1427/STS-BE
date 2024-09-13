using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.Role;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<RoleDTO, Role>()
                    //.ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    //.ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, RoleDTO>().ReverseMap();
            CreateMap<Pagination<Role>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}