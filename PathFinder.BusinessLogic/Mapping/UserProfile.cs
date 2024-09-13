using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.User;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<UserDTO, User>()
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, UserDTO>().ReverseMap();
            CreateMap<User, GetPageDefinitionDTO>()
                .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.UserName))
                .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UserName))
                .ReverseMap();
            CreateMap<Pagination<User>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}
