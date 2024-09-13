using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.NGO;
using PathFinder.DataTransferObjects.DTOs.Partner;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.BusinessLogic.Mapping
{
    public class NGOProfile : Profile
    {
        public NGOProfile()
        {
            CreateMap<NonGovermntalOrgniszation, GetLookUpNgoDTO>().ReverseMap();
            CreateMap<NonGovermntalOrgniszation, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<PartnerDTO, NonGovermntalOrgniszation>().ReverseMap();
            CreateMap<Pagination<NonGovermntalOrgniszation>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();


        }
    }
}
