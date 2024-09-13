using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.CompanyCategory;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;
namespace PathFinder.BusinessLogic.Mapping
{
    public class CompanyCategoryProfile : Profile
    {
        public CompanyCategoryProfile()
        {
            CreateMap<CompanyCategory, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<CompanyCategoryDTO, CompanyCategory>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, CompanyCategoryDTO>().ReverseMap();
            CreateMap<CompanyCategory, GetLookUpDefinitionDTO>()
            .ForMember(dest => dest.CreatedBy, ex => ex.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(dest => dest.UpdatedBy, ex => ex.MapFrom(src => src.UpdatedByUser.UserName))
            .ReverseMap();
            CreateMap<Pagination<CompanyCategory>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}