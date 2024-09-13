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
                    .ReverseMap();
            CreateMap<GetLookUpDefinitionDTO, GovernorateDTO>().ReverseMap();

            CreateMap<Pagination<Governorate>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}