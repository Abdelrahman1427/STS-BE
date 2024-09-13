using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.Certification;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class CertificationProfile : Profile
    {
        public CertificationProfile()
        {
            CreateMap<Certification, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<CertificationDTO, Certification>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, CertificationDTO>().ReverseMap();
            CreateMap<Certification, GetLookUpDefinitionDTO>()
                .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
                .ReverseMap();
            CreateMap<Pagination<Certification>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}