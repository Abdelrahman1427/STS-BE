using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.EntrepriseOrigin;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class EnterpriseOriginProfile : Profile
    {
        public EnterpriseOriginProfile()
        {
            CreateMap<EnterpriseOrigin, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<EnterpriseOriginDTO, EnterpriseOrigin>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, EnterpriseOriginDTO>().ReverseMap();
            CreateMap<EnterpriseOrigin, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<EnterpriseOrigin>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}