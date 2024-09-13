using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.UserType;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            CreateMap<UserType, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<UserTypeDTO, UserType>()
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, UserTypeDTO>().ReverseMap();
            CreateMap<Pagination<UserType>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}
