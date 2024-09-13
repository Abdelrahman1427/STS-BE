using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.EntrepriseType;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class EnterpriseTypeProfile : Profile
    {
        public EnterpriseTypeProfile()
        {
            CreateMap<EnterpriseType, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<EnterpriseTypeDTO, EnterpriseType>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, EnterpriseTypeDTO>().ReverseMap();
            CreateMap<EnterpriseType, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<EnterpriseType>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}