using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.CompanyStatus;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class CompanyStatusProfile : Profile
    {
        public CompanyStatusProfile()
        {
            CreateMap<CompanyStatus, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<CompanyStatusDTO, CompanyStatus>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, CompanyStatusDTO>().ReverseMap();
            CreateMap<CompanyStatus, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<CompanyStatus>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}