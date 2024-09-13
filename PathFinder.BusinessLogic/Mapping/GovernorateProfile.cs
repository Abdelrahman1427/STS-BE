using AutoMapper;
using STS.Core.Entities;
using STS.DataTransferObjects.DTOs.Governorate;
using STS.DataTransferObjects.DTOs.Shared.Request;
using STS.DataTransferObjects.Helpers;

namespace STS.BusinessLogic.Mapping
{
    public class GovernorateProfile : Profile
    {
        public GovernorateProfile()
        {
            CreateMap<Governorate, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<GovernorateDTO, Governorate>()
                    .ForMember(e => e.CreatedBy, ex => ex.Condition(src => src.Id == null))
                    .ForMember(e => e.CreatedDate, ex => ex.Condition(src => src.Id == null))
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, GovernorateDTO>().ReverseMap();

            CreateMap<Pagination<Governorate>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}